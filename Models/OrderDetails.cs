using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public decimal OrderTotal { get; set; }

        [Display(Name ="Order")]
        public int OrederId { get; set; }
        [ForeignKey("OrederId")]
        public virtual Order Order { get; set; }

        [Display(Name = "Cart")]
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual ShoppingCart ShoppingCart { get; set; }

    }
}
