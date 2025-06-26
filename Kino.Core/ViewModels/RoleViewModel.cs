using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public required string RoleName { get; set; }
    }
}
