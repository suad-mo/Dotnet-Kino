using Kino.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class ProjekcijaViewModel
    {
        public ProjekcijaViewModel() { }

        public int Id { get; set; }

        [Required]
        public DateTime VrijemePocetka { get; set; }

        public DateTime VrijemeZavrsetka { get; set; }

        [Required]
        public float Cijena { get; set; }

        public required Sala Sala { get; set; }

        public int SalaId { get; set; }

        public required Film Film { get; set; }

        public int FilmId { get; set; }
    }
}
