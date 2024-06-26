using System.ComponentModel.DataAnnotations;
using WeddingGem.Data.Entites;

namespace WeddingGem.Dashboard.Models
{
    public class UserRoleView
    {
        public string id { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }
        public string PlanName { get; set; }
        public List<Packages> Packages { get; set; }
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
