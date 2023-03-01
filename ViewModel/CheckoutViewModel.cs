using Ecommerce.Models;

namespace Ecommerce.ViewModel
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel()
        {
            ShoppingCarts = new List<ShoppingCart>();
            Order = new Order();
        }
        public List<ShoppingCart> ShoppingCarts { get; set; }
        public Order Order { get; set; }
    }
}
