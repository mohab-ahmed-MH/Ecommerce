using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly EcommerceContext db;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(EcommerceContext context, UserManager<AppUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var checkoutModel = new CheckoutViewModel();
            var user = await _userManager.GetUserAsync(User);
            var shoppingCarts = db.ShoppingCarts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToList();
            checkoutModel.ShoppingCarts = shoppingCarts;

            return View(checkoutModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel viewModel)
        {
            var model = viewModel.Order;
            var user = await _userManager.GetUserAsync(User);
            var cart = db.ShoppingCarts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToList();
            if (cart != null)
            {
                model.OrderDetails = cart.Select(x => new OrderDetails()
                {
                    CartId = x.Id
                }).ToList();
            }

            model.OrderNum = GetOrdernNo();
            db.Orders.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public string GetOrdernNo()
        {
            int num = db.Orders.Count() + 1;
            return num.ToString("000");
        }
    }
}
