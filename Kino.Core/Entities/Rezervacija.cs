using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kino.Core.Entities
{
    public class Rezervacija
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool Placeno { get; set; }

        [ForeignKey(nameof(Sjediste))]
        public int SjedisteId { get; set; }

        public required Sjediste Sjediste { get; set; }

        [ForeignKey(nameof(Projekcija))]
        public int ProjekcijaId { get; set; }

        public required Projekcija Projekcija { get; set; }

        [ForeignKey(nameof(UserAccount))]
        public required string UserId { get; set; }

        public required UserAccount User { get; set; }
    }
}
