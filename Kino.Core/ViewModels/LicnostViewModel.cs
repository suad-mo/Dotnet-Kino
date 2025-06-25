using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kino.Core.ViewModels
{
    public class LicnostViewModel
    {
        public LicnostViewModel()
        {
            // Initialize non-nullable properties to default values to avoid CS8618 errors.  
            ImePrezime = string.Empty;
            Filmovi = new List<FilmViewModel>();
        }

        public int Id { get; set; }

        [Required]
        public string ImePrezime { get; set; }

        [Required]
        public bool IsGlumac { get; set; }

        [Required]
        public bool IsRedatelj { get; set; }

        public List<FilmViewModel> Filmovi { get; set; }
    }
}
