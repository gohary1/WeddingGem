using System.ComponentModel.DataAnnotations;

namespace WeddingGem.Dashboard.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email field is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
