using AutoMapper;
using Kino.Core.Entities;
using Kino.Core.Pagination;
using Kino.Core.ViewModels;
using Kino.Repositories.Filmovi;

namespace Kino.Services.Filmovi
{
    public class FilmService(IFilmRepository repository, IMapper mapper) : IFilmService
    {
        private readonly IFilmRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public List<FilmViewModel> GetMovies(int pageNumber, int pageSize, string searchType, string searchQuery)
        {
            var moviesList = _repository.GetMovies(pageNumber, pageSize, searchType, searchQuery);

            return moviesList;
        }

        public FilmViewModel? GetMovie(int id, string language)
        {
            var movie = _repository.GetMovie(id);

            if (movie == null)
                return null;

            var translatedMovie = _repository.GetTranslatedMovie(id, language);

            var mappedMovie = _mapper.Map<FilmViewModel>(movie);

            if (translatedMovie != null)
            {
                mappedMovie.Naziv = translatedMovie.Naziv ?? mappedMovie.Naziv;
                mappedMovie.Opis = translatedMovie.Opis ?? mappedMovie.Opis;
            }

            return mappedMovie;
        }

        public FilmViewModel? SaveMovie(FilmWithoutLicnost film)
        {
            var movie = _repository.SaveMovie(film);

            if (movie == null)
                return null;

            return _mapper.Map<FilmViewModel>(movie);
        }

        public FilmViewModel? DeleteMovie(int id)
        {
            var movie = _repository.DeleteMovie(id);
            if (movie == null)
                return null;

            return _mapper.Map<FilmViewModel>(movie);
        }

        public PaginationMetadata GetPaginationMetadata(int pageSize)
        {
            var count = _repository.GetMovieCount();

            var pageCount = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginationMetadata(count, pageCount);
        }

        public List<FilmViewModel> GetMoviesAll()
        {
            var movies = _repository.GetMoviesAll();
            if (movies == null || movies.ToArray().Length == 0)
                return new List<FilmViewModel>();

            return _mapper.Map<List<FilmViewModel>>(movies);
        }

        public List<FilmViewModel> GetActiveMovies(int pageNumber, int pageSize, string language)
        {
            var moviesList = _repository.GetActiveMovies(pageNumber, pageSize, language);

            return moviesList;
        }

        public FilmTranslation AddFilmTranslation(FilmTranslation filmTranslation)
        {
            return _repository.AddFilmTranslation(filmTranslation);
        }
    }
}
