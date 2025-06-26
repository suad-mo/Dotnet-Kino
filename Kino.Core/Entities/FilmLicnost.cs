using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kino.Core.Entities
{
    public class FilmLicnost
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Licnost))]
        public int LicnostId { get; set; }

        // Adding 'required' modifier to fix CS8618  
        public required Licnost Licnost { get; set; }

        [ForeignKey(nameof(Film))]
        public int FilmId { get; set; }

        // Adding 'required' modifier to ensure Film is non-nullable  
        public required Film Film { get; set; }
    }
}
