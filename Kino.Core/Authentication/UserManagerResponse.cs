namespace Kino.Core.Authentication
{
    /// <summary>
    /// Predstavlja standardizovani odgovor za operacije korisničkog menadžmenta (npr. prijava, registracija).
    /// Sadrži informacije o uspehu, poruke, eventualne greške i JWT token.
    /// </summary>
    public class UserManagerResponse
    {
        /// <summary>
        /// Opisna poruka o rezultatu operacije (npr. "Uspješna prijava").
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// JWT token generisan prilikom uspešne autentifikacije.
        /// </summary>
        public string JwtToken { get; set; } = string.Empty;

        /// <summary>
        /// Oznaka da li je operacija uspešno izvršena.
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// Poruka o grešci, ukoliko je došlo do neuspeha.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// Datum i vreme isteka JWT tokena (ako postoji).
        /// </summary>
        public DateTime? ExpireDate { get; set; } = null;
    }
}
