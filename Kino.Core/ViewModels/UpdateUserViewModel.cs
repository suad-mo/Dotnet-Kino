using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class UpdateUserViewModel
    {
        public required string Id { get; set; }
        
        [Required]
        public required string Ime { get; set; }

        [Required]
        public required string Prezime { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
