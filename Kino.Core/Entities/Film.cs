using Kino.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kino.Core.Entities
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public required string Naziv { get; set; }

        public string? Opis { get; set; }

        public string? UrlFotografije { get; set; }

        [RegularExpression(@"^(19|20)[\d]{2,2}$")]
        public int Godina { get; set; }

        public JezikEnum Jezik { get; set; }

        public bool IsActive { get; set; }

        public int Trajanje { get; set; } // Trajanje u minutama

        public List<FilmLicnost> Licnosti { get; set; } = new List<FilmLicnost>();
    }
}
