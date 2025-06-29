﻿using System.ComponentModel.DataAnnotations;

namespace Kino.Core.Entities
{
    public class Licnost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string ImePrezime { get; set; }

        [Required]
        public bool IsGlumac { get; set; }

        [Required]
        public bool IsRedatelj { get; set; }

        // Fix: Initialize the Filmovi property to an empty list to ensure it is not null.  
        public List<FilmLicnost> Filmovi { get; set; } = new List<FilmLicnost>();
    }
}
