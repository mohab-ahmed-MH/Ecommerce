using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly EcommerceContext db;
        public ProductsController(EcommerceContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var products = db.Products.Include(c => c.Category).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Product model,IFormFile File)
        {
            if (File != null) 
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string productfolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\product");
                if (!Directory.Exists(productfolder)) 
                   Directory.CreateDirectory(productfolder);
                string filePathImage = Path.Combine(productfolder, imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                    await File.CopyToAsync(stream);
                }
                model.Image = imageName;
            }
            db.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = db.Products.Find(id);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id ,Product model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string productfolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\product");
                if (!Directory.Exists(productfolder))
                    Directory.CreateDirectory(productfolder);
                string filePathImage = Path.Combine(productfolder, imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                     File.CopyTo(stream);
                }
                model.Image = imageName;
            }
            else
            {
                model.Image = model.Image;
            }
            db.Update(model);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(id != null )
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
