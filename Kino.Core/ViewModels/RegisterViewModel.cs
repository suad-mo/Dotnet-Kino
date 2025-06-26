using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public required string Ime { get; set; }

        [Required]
        public required string Prezime { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public required string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public required string PasswordConfirmation { get; set; }
    }
}
