using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly EcommerceContext db;
        public HomeController(EcommerceContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Categories = db.Categories.ToList(),
                Products = db.Products.ToList(),
            };
            return View(model);
        }


        public IActionResult Shop()
        {
            var shop = db.Products.ToList();
            return View(shop);
        }


        public IActionResult CategoryProduct(int? id)
        {
            var products = db.Products.Where(a => a.CategoryId == id)
                .ToList();
            return View(products);
        }

        public IActionResult ProductDetailes(int? id)
        {
            var products = db.Products.Include(x => x.Category).FirstOrDefault(p => p.Id== id);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Content()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Content(Contact model)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}