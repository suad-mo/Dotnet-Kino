using Kino.Core.Entities;
using Kino.Core.ViewModels;

namespace Kino.Repositories.Filmovi
{
    public interface IFilmRepository
    {
        List<FilmViewModel> GetMovies(int pageNumber, int pageSize, string searchType, string searchQuery);
        Film? GetMovie(int id);
        Film SaveMovie(FilmWithoutLicnost film);
        Film? DeleteMovie(int id);
        int GetMovieCount();
        List<Film> GetMoviesAll();
        List<FilmViewModel> GetActiveMovies(int pageNumber, int pageSize, string language);
        FilmTranslation? GetTranslatedMovie(int filmId, string language);
        FilmTranslation AddFilmTranslation(FilmTranslation filmTranslation);
    }
}
