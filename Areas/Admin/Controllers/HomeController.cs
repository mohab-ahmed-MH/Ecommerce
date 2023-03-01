using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly EcommerceContext db;
        public HomeController(EcommerceContext context)
        {
            db = context; 
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ContactMessage()
        {
            var message = db.Contacts.ToList();
            return View(message);
        }

        public IActionResult DeleteMessage(int? id)
        {
            if (id != null)
            {
                var message = db.Contacts.Find(id);
                db.Contacts.Remove(message);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(ContactMessage));
        }


        public IActionResult Order()
        {
            return View();
        }

    }
}
