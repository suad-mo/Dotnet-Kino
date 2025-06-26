using Kino.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class FilmWithoutLicnost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv filma je obavezno polje.")]
        public required string Naziv { get; set; }

        public string? Opis { get; set; }

        public string? UrlFotografije { get; set; }

        [RegularExpression(@"^(19|20)[\d]{2,2}$", ErrorMessage = "Godina mora biti u YYYY formatu.")]
        public int Godina { get; set; }

        public JezikEnum Jezik { get; set; }

        public int Trajanje { get; set; }

        public bool IsActive { get; set; }
    }
}
