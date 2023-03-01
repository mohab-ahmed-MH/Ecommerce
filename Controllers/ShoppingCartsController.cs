using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly EcommerceContext db;
        private readonly UserManager<AppUser> _userManager;

        public ShoppingCartsController(EcommerceContext context,UserManager<AppUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> ShoppingCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = db.ShoppingCarts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToList();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(ShoppingCart model, int qty,string siz)
        {
            
            var product = db.Products.FirstOrDefault(p=>p.Id == model.ProductId );
            var user = await _userManager.GetUserAsync(User);
            var cart = new ShoppingCart 
            {
                UserId = user.Id,
                ProductId = product.Id,
                Quantity = qty,
                Size = siz,
            };
            var shopcart = db.ShoppingCarts.FirstOrDefault(u => u.UserId == user.Id && u.ProductId == model.ProductId);
            if(qty <= 0)
            {
                qty = 1;
            }
            if(shopcart == null)
                db.Add(cart);
            else
                shopcart.Quantity += qty;
            db.SaveChanges();
            return RedirectToAction(nameof(ShoppingCart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int? id)
        {
            
            var user = await _userManager.GetUserAsync(User);
            
            var shopcart = db.ShoppingCarts.FirstOrDefault(u => u.UserId == user.Id && u.Id == id);

            if(shopcart != null)
                db.ShoppingCarts.Remove(shopcart);
           
            db.SaveChanges();

            return RedirectToAction(nameof(ShoppingCart));
        }

    }
}