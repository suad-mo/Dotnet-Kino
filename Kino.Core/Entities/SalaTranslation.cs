namespace Kino.Core.Entities
{
    public class SalaTranslation
    {
        public int SalaId { get; set; }
        public required string Language { get; set; }
        public required string Naziv { get; set; }
    }
}
