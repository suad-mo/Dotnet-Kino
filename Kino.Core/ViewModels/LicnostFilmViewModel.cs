using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class LicnostFilmViewModel
    {
        [Required]
        public int LicnostId { get; set; }

        [Required]
        public List<int> Filmovi { get; set; } = new List<int>(); // Initialize the property to avoid null value
    }
}
