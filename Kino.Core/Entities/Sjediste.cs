using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kino.Core.Entities
{
    public class Sjediste
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RedniBroj { get; set; }

        [ForeignKey(nameof(Red))]
        public int RedId { get; set; }

        public Red Red { get; set; }

        [ForeignKey(nameof(VrstaSjedista))]
        public int VrstaSjedistaId { get; set; }

        public required VrstaSjedista VrstaSjedista { get; set; }
    }
}
