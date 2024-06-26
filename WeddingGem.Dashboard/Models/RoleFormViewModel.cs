using System.ComponentModel.DataAnnotations;

namespace WeddingGem.Dashboard.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "please insert the role name")]
        public string Name { get; set; }
    }
}
