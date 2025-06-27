using AutoMapper;
using Kino.Core.Entities;
using Kino.Core.Pagination;
using Kino.Core.ViewModels;
using Kino.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kino.Repositories.Projekcije
{
    public class ProjekcijaRepository : IProjekcijaRepository
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IMapper _mapper;

        private int projekcijeCount;
        public ProjekcijaRepository(DatabaseContext context, IMapper mapper)
        {
            _databaseContext = context;
            _mapper = mapper;
        }

        public List<ProjekcijaViewModel> GetProjekcije(int pageNumber, int pageSize, string search_query, string search_type)
        {
            IQueryable<ProjekcijaViewModel> result;
            int parsedQueryValue;
            bool canParse;

            switch (search_type)
            {
                case "film":
                    result = _databaseContext.Projekcija.Where(p => search_query == null || search_query == "" || p.Film.Naziv.ToLower().StartsWith(search_query.ToLower()))
                                                        .Include(p => p.Film).Include(p => p.Sala)
                                                        .Select(p => _mapper.Map<ProjekcijaViewModel>(p));
                    break;

                case "id":
                    parsedQueryValue = 0;
                    canParse = search_query != null && search_query != "" && int.TryParse(search_query, out parsedQueryValue);
                    if (canParse && parsedQueryValue != 0)
                        result = _databaseContext.Projekcija.Where(p => p.Id.Equals(parsedQueryValue))
                                                            .Include(p => p.Film).Include(p => p.Sala)
                                                            .Select(p => _mapper.Map<ProjekcijaViewModel>(p));
                    else
                        result = _databaseContext.Projekcija.Include(p => p.Film).Include(p => p.Sala)
                                                            .Select(p => _mapper.Map<ProjekcijaViewModel>(p));
                    break;

                case "filmId":
                    parsedQueryValue = 0;
                    canParse = search_query != null && search_query != "" && int.TryParse(search_query, out parsedQueryValue);
                    if (canParse && parsedQueryValue != 0)
                        result = _databaseContext.Projekcija.Where(p => p.FilmId.Equals(parsedQueryValue))
                                                            .Include(p => p.Film).Include(p => p.Sala)
                                                            .Select(p => _mapper.Map<ProjekcijaViewModel>(p));
                    else
                        result = _databaseContext.Projekcija.Include(p => p.Film).Include(p => p.Sala)
                                                            .Select(p => _mapper.Map<ProjekcijaViewModel>(p));
                    break;

                default:
                    result = _databaseContext.Projekcija.Include(p => p.Film).Include(p => p.Sala)
                                                            .Select(p => _mapper.Map<ProjekcijaViewModel>(p));
                    break;
            }

            this.projekcijeCount = result.Count();
            return PaginatedList<ProjekcijaViewModel>.Create(result.AsNoTracking(), pageNumber, pageSize);
        }

        public Projekcija? GetProjekcija(int id)
        {
            if (!_databaseContext.Projekcija.Any(p => p.Id == id))
                return null;

            return _databaseContext.Projekcija.Where(p => p.Id == id).Include(p => p.Film).Include(p => p.Sala).FirstOrDefault();
        }

        public Projekcija? SaveProjekcija(ProjekcijaViewModel projekcija)
        {
            var film = _databaseContext.Film.Find(projekcija.FilmId);

            if (film == null)
                return null;

            var sala = _databaseContext.Sala.Find(projekcija.SalaId);

            if (sala == null)
                return null;

            var projekcijaToSave = _databaseContext.Projekcija.Find(projekcija.Id);

            if (projekcijaToSave == null)
            {
                projekcijaToSave = new Projekcija
                {
                    VrijemePocetka = projekcija.VrijemePocetka,
                    VrijemeZavrsetka = projekcija.VrijemePocetka.AddMinutes(film.Trajanje),
                    Cijena = projekcija.Cijena,
                    FilmId = film.Id,
                    Film = film, // Fix for CS9035: Required member 'Projekcija.Film' must be set
                    SalaId = sala.Id,
                    Sala = sala  // Fix for CS9035: Required member 'Projekcija.Sala' must be set
                };

                _databaseContext.Add(projekcijaToSave);
            }
            else
            {
                projekcijaToSave.VrijemePocetka = projekcija.VrijemePocetka;
                projekcijaToSave.VrijemeZavrsetka = projekcija.VrijemePocetka.AddMinutes(film.Trajanje);
                projekcijaToSave.Cijena = projekcija.Cijena;
                projekcijaToSave.FilmId = film.Id;
                projekcijaToSave.Film = film; // Fix for CS9035: Required member 'Projekcija.Film' must be set
                projekcijaToSave.SalaId = sala.Id;
                projekcijaToSave.Sala = sala; // Fix for CS9035: Required member 'Projekcija.Sala' must be set
            }

            _databaseContext.SaveChanges();
            return projekcijaToSave;
        }

        public int GetProjekcijeCount()
        {
            return this.projekcijeCount;
        }

        public List<Projekcija> GetAllProjekcijeForMovie(int filmId, string language)
        {
            var projekcije = _databaseContext.Projekcija
                .Where(p => p.FilmId == filmId)
                .Include(p => p.Film)
                .Include(p => p.Sala)
                .OrderBy(p => p.VrijemePocetka)
                .ToList();

            projekcije.ForEach(p =>
            {
                var translatedSala = GetTranslatedSala(p.Sala.Id, language);
                if (translatedSala != null)
                {
                    p.Sala.Naziv = translatedSala.Naziv;
                }

                var translatedMovie = GetTranslatedMovie(p.Film.Id, language);
                if (translatedMovie != null)
                {
                    p.Film.Naziv = translatedMovie.Naziv ?? p.Film.Naziv; // Fix for CS8601
                    p.Film.Opis = translatedMovie.Opis ?? p.Film.Opis;   // Fix for CS8601
                }
            });

            return projekcije;
        }

        private SalaTranslation? GetTranslatedSala(int salaId, string language)
        {
            return _databaseContext.SalaTranslation.FirstOrDefault(st => st.SalaId == salaId && st.Language == language);
        }

        private FilmTranslation? GetTranslatedMovie(int filmId, string language)
        {
            return _databaseContext.FilmTranslation.FirstOrDefault(ft => ft.FilmId == filmId && ft.Language == language);
        }
    }
}
