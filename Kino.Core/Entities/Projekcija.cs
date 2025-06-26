using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kino.Core.Entities
{
    public class Projekcija
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime VrijemePocetka { get; set; }

        [Required]
        public DateTime VrijemeZavrsetka { get; set; }

        [Required]
        public float Cijena { get; set; }

        [ForeignKey(nameof(Sala))]
        public int SalaId { get; set; }

        public required Sala Sala { get; set; }

        [ForeignKey(nameof(Film))]
        public int FilmId { get; set; }

        public required Film Film { get; set; }
    }
}
