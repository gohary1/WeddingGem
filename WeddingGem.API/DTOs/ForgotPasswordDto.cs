using System.ComponentModel.DataAnnotations;

namespace WeddingGem.API.DTOs
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "enter a valid Email Address")]
        public string Email { get; set; }
    }
}
