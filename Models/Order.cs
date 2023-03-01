using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNum { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email  { get; set; }
        [Required]
        public string PhoneNum { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
