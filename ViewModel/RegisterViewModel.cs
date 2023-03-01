using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModel
{
    public class RegisterViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="your password nessd to include both lower and upper case characters.")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="The password and Confirmation password do not match.")]
        public string? ConfiremPassword { get; set; }

    }
}
