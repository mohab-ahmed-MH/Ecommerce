using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
