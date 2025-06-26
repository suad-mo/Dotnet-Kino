using Kino.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class ArtiklViewModel
    {
        // Constructor updated to initialize non-nullable properties with default values  
        public ArtiklViewModel()
        {
            Naziv = string.Empty; // Initialize with a default non-null value  
            KategorijaArtikl = new KategorijaArtikl
            {
                Naziv = string.Empty // Initialize required member 'Naziv'  
            };
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv artikla je obavezno polje.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Cijena artikla je obavezno polje.")]
        public double Cijena { get; set; }

        [Required(ErrorMessage = "Kolicina na stanju artikla je obavezno polje.")]
        public int KolicinaNaStanju { get; set; }

        public KategorijaArtikl KategorijaArtikl { get; set; }

        public int KategorijaId { get; set; }
    }
}
