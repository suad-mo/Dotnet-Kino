﻿using System.ComponentModel.DataAnnotations;

namespace Kino.Core.ViewModels
{
    public class LicnostViewModel
    {
        public LicnostViewModel() { }

        public int Id { get; set; }

        [Required]
        public required string ImePrezime { get; set; }

        [Required]
        public bool IsGlumac { get; set; }

        [Required]
        public bool IsRedatelj { get; set; }

        public List<FilmViewModel> Filmovi { get; set; } = new List<FilmViewModel>();
    }
}
