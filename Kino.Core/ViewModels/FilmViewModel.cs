using Kino.Core.Enums;
//using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Kino.Core.ViewModels
{
    public class FilmViewModel
    {
        public FilmViewModel()
        {
            // Initialize non-nullable properties to default values to avoid CS8618 errors
            Naziv = string.Empty;
            Opis = string.Empty;
            UrlFotografije = string.Empty;
            Licnosti = new List<LicnostViewModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv filma je obavezno polje.")]
        public string Naziv { get; set; }

        public string Opis { get; set; }

        public string UrlFotografije { get; set; }

        [RegularExpression(@"^(19|20)[\d]{2,2}$", ErrorMessage = "Godina mora biti u YYYY formatu.")]
        public int Godina { get; set; }

        public JezikEnum Jezik { get; set; }

        public int Trajanje { get; set; }

        public bool IsActive { get; set; }
        public List<LicnostViewModel> Licnosti { get; set; }
    }
}
