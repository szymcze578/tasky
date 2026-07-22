using System.ComponentModel.DataAnnotations;

namespace Tasky.API.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [StringLength(100)]
        public required string Password { get; set; }

        [StringLength(100)]
        public string? Street { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [RegularExpression(@"^\d{2}-\d{3}$|\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string? PostalCode { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }
    }
}
