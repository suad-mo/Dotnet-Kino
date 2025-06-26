using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class KategorijaArtiklViewModel
    {
        public KategorijaArtiklViewModel() { }

        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv artikla je obavezno polje.")]
        public required string Naziv { get; set; }
    }
}
