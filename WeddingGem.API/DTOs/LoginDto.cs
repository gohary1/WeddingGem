using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
