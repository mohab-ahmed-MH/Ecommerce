using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Contact
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }

    }
}
