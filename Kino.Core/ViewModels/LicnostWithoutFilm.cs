using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class LicnostWithoutFilm
    {
        public int Id { get; set; }

        [Required]
        public required string ImePrezime { get; set; }

        [Required]
        public bool IsGlumac { get; set; }

        [Required]
        public bool IsRedatelj { get; set; }
    }
}
