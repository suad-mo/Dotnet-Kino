using Kino.Core.Entities;
using Kino.Core.Pagination;
using Kino.Core.ViewModels;

namespace Kino.Services.Filmovi
{
    public interface IFilmService
    {
        List<FilmViewModel> GetMovies(int pageNumber, int pageSize, string searchType, string searchQuery);
        FilmViewModel? GetMovie(int id, string language);
        FilmViewModel? SaveMovie(FilmWithoutLicnost film);
        FilmViewModel? DeleteMovie(int id);
        PaginationMetadata GetPaginationMetadata(int pageSize);
        List<FilmViewModel> GetMoviesAll();
        List<FilmViewModel> GetActiveMovies(int pageNumber, int pageSize, string language);
        FilmTranslation AddFilmTranslation(FilmTranslation filmTranslation);
    }
}
