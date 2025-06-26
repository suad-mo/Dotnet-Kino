using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kino.Core.Entities
{
    public class Red
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RedniBroj { get; set; }

        [Required]
        public int BrojSjedišta { get; set; }

        [ForeignKey(nameof(Sala))]
        public int SalaId { get; set; }

        public required Sala Sala { get; set; }
    }
}
