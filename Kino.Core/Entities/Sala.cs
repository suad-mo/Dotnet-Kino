using System.ComponentModel.DataAnnotations;

namespace Kino.Core.Entities
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Naziv { get; set; }

        [Required]
        public int Kapacitet { get; set; }
    }
}
