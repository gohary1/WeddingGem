using System.ComponentModel.DataAnnotations;
using WeddingGem.Data.Entites;

namespace WeddingGem.Dashboard.Models
{
    public class EditVendorModel
    {
        [RegularExpression("^[a-zA-Z]{3,20}$", ErrorMessage = "Please enter Your Name Correctly")]
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "enter a valid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is Required")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Birthday is Required")]

        public string Birthday { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, ErrorMessage = "enter valid phone number", MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public Packages Package { get; set; }
    }
}
