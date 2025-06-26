namespace Kino.Core.Entities
{
    public class FilmTranslation
    {
        public int FilmId { get; set; }
        public required string Language { get; set; }
        public string? Opis { get; set; }
        public string? Naziv { get; set; }
    }
}
