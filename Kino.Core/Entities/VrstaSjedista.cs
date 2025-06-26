using System.ComponentModel.DataAnnotations;

namespace Kino.Core.Entities
{
    public class VrstaSjedista
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Naziv { get; set; }
    }
}
