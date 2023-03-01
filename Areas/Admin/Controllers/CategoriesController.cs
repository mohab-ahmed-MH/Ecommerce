using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly EcommerceContext db;
        public CategoriesController(EcommerceContext context)
        {
            db= context;
        }

        public IActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }
        
        [HttpGet]
        public IActionResult Create() 
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model,IFormFile File)
        {
            if(File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg"   ;
                string categoryfolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\category");
                if (!Directory.Exists(categoryfolder))
                    Directory.CreateDirectory(categoryfolder);
                string filePathImage = Path.Combine(categoryfolder, imageName);
                using(var streem = System.IO.File.Create(filePathImage)) 
                {
                    await File.CopyToAsync(streem);
                }
                model.Photo = imageName;
            }
            db.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var categories = db.Categories.Find(id);
            return View(categories);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string categoryfolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\category");
                if (!Directory.Exists(categoryfolder))
                    Directory.CreateDirectory(categoryfolder);
                string filePathImage = Path.Combine(categoryfolder, imageName);
                using (var streem = System.IO.File.Create(filePathImage))
                {
                     File.CopyToAsync(streem);
                }
                model.Photo = imageName;
            }
            else
            {
                model.Photo = model.Photo;
            }
            db.Update(model);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? id)
        {
            if(id != null)
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
