﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kino.Core.Entities
{
    public class Artikl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Naziv { get; set; } // Fix: Added 'required' modifier to ensure initialization.

        [ForeignKey(nameof(KategorijaArtikl))]
        public int KategorijaId { get; set; }

        public required KategorijaArtikl KategorijaArtikl { get; set; } // Already fixed.

        [Required]
        public double Cijena { get; set; }

        [Required]
        public int KolicinaNaStanju { get; set; }
    }
}
