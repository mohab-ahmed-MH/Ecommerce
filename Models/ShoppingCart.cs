using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public string? Total { get; set; }


        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

    }
}
