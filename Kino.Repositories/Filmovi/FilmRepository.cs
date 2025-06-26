using AutoMapper;
using Kino.Core.Entities;
using Kino.Core.Pagination;
using Kino.Core.ViewModels;
using Kino.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kino.Repositories.Filmovi
{
    public class FilmRepository: IFilmRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        private int movieCount;

        public FilmRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FilmViewModel> GetMovies(int pageNumber, int pageSize, string searchType, string searchQuery)
        {
            IQueryable<FilmViewModel> result;

            switch (searchType)
            {
                case "film":
                    result = _context.Film.Include(x => x.Licnosti).ThenInclude(x => x.Licnost)
                                          .Where(f => searchQuery == null || searchQuery == "" || f.Naziv.ToLower().StartsWith(searchQuery.ToLower()))
                                          .Select(f => _mapper.Map<FilmViewModel>(f));
                    break;

                case "id":
                    int parsedQueryValue = 0;
                    bool canParse = searchQuery != null && searchQuery != "" && int.TryParse(searchQuery, out parsedQueryValue);
                    if (canParse && parsedQueryValue != 0)
                        result = _context.Film.Include(x => x.Licnosti).ThenInclude(x => x.Licnost).Where(f => f.Id.Equals(parsedQueryValue))
                                              .Select(f => _mapper.Map<FilmViewModel>(f));
                    else
                        result = _context.Film.Include(x => x.Licnosti).ThenInclude(x => x.Licnost).Select(f => _mapper.Map<FilmViewModel>(f));
                    break;

                default:
                    result = _context.Film.Include(x => x.Licnosti).ThenInclude(x => x.Licnost).Select(f => _mapper.Map<FilmViewModel>(f));
                    break;
            }

            this.movieCount = result.Count();
            return PaginatedList<FilmViewModel>.Create(result.AsNoTracking(), pageNumber, pageSize);
        }

        public Film? GetMovie(int id)
        {
            var movie = _context.Film.Include(x => x.Licnosti).ThenInclude(x => x.Licnost).Where(x => x.Id == id).FirstOrDefault();

            return movie;
        }

        public Film SaveMovie(FilmWithoutLicnost film)
        {
            var movie = _context.Film.Find(film.Id);
            if (movie == null)
            {
                movie = new Film
                {
                    Naziv = film.Naziv,
                    Opis = film.Opis,
                    Jezik = film.Jezik,
                    Godina = film.Godina,
                    UrlFotografije = film.UrlFotografije,
                    Trajanje = film.Trajanje,
                    IsActive = film.IsActive
                };
                _context.Film.Add(movie);
            }
            else
            {
                movie.Naziv = film.Naziv;
                movie.Opis = film.Opis;
                movie.Godina = film.Godina;
                movie.Jezik = film.Jezik;
                movie.UrlFotografije = film.UrlFotografije;
                movie.Trajanje = film.Trajanje;
                movie.IsActive = film.IsActive;
            }

            _context.SaveChanges();
            return movie;
        }

        public Film? DeleteMovie(int id)
        {
            var movie = _context.Film.Find(id);
            if (movie == null) return null;

            _context.Film.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

        public int GetMovieCount()
        {
            return movieCount;
        }

        public List<Film> GetMoviesAll()
        {
            return _context.Film.Include(x => x.Licnosti).ThenInclude(x => x.Licnost).ToList();
        }

        public List<FilmViewModel> GetActiveMovies(int pageNumber, int pageSize, string language)
        {
            var mappedMovies = _context.Film.Include(x => x.Licnosti).Where(m => m.IsActive).Select(m => _mapper.Map<FilmViewModel>(m));

            this.movieCount = mappedMovies.Count();
            var paginatedList = PaginatedList<FilmViewModel>.Create(mappedMovies.AsNoTracking(), pageNumber, pageSize);
            paginatedList.ForEach(m =>
            {
                var translatedMovie = GetTranslatedMovie(m.Id, language);

                if (translatedMovie != null)
                {
                    if (translatedMovie.Opis != null && !string.IsNullOrEmpty(translatedMovie.Opis))
                        m.Opis = translatedMovie.Opis;

                    if (translatedMovie.Naziv != null && !string.IsNullOrEmpty(translatedMovie.Naziv))
                        m.Naziv = translatedMovie.Naziv;
                }
            });

            return paginatedList;
        }

        public FilmTranslation? GetTranslatedMovie(int filmId, string language)
        {
            return _context.FilmTranslation.FirstOrDefault(ft => ft.FilmId == filmId && ft.Language == language);
        }

        public FilmTranslation AddFilmTranslation(FilmTranslation filmTranslation)
        {
            var translation = _context.FilmTranslation.Find(filmTranslation.FilmId, filmTranslation.Language);

            if (translation == null)
            {
                translation = new FilmTranslation
                {
                    FilmId = filmTranslation.FilmId,
                    Language = filmTranslation.Language,
                    Naziv = filmTranslation.Naziv,
                    Opis = filmTranslation.Opis
                };

                _context.FilmTranslation.Add(translation);
            }
            else
            {
                translation.Naziv = filmTranslation.Naziv;
                translation.Opis = filmTranslation.Opis;
            }

            _context.SaveChanges();
            return translation;
        }

    }
}
