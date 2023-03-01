using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Photo { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        
        public virtual ICollection<Product> Product { get; set; }

    }
}
