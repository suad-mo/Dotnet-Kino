using System.ComponentModel.DataAnnotations;

namespace Kino.Core.Entities
{
    /// <summary>
    /// Predstavlja kategoriju artikla u sistemu (npr. piće, grickalice, itd.).
    /// </summary>
    public class KategorijaArtikl
    {
        /// <summary>
        /// Jedinstveni identifikator kategorije artikla.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Naziv kategorije artikla (obavezno polje).
        /// </summary>
        [Required]
        public required string Naziv { get; set; }
    }
}
