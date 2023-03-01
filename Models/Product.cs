using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descrrption { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
