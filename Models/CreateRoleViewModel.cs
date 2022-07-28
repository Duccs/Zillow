using System.ComponentModel.DataAnnotations;

namespace Zillow.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
