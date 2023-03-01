using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly EcommerceContext db;

        public UsersController(EcommerceContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult Block(int? id)
        {
            if (id != null)
            {
                var user = db.Users.Find(id);
                user.LockoutEnd = DateTime.Now.AddDays(30);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Active(int? id)
        {
            if (id != null)
            {
                var user = db.Users.Find(id);
                user.LockoutEnd = DateTime.Now.AddDays(-1);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
