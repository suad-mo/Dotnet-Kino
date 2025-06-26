using Microsoft.EntityFrameworkCore;
using Kino.Core.Entities;
using Kino.Core.ViewModels;
using Kino.Infrastructure.Database;

namespace Kino.Repositories.Licnosti
{
    public class LicnostRepository : ILicnostRepository
    {
        private readonly DatabaseContext _context;
        public LicnostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Licnost? AddFilm(LicnostFilmViewModel filmovi)
        {
            var licnost = _context.Licnost.Find(filmovi.LicnostId);

            if (licnost == null) return null;

            var filmoviUKojojJeLicnost = _context.FilmLicnost
                .Where(x => x.LicnostId == filmovi.LicnostId)
                .Select(x => x.FilmId)
                .ToList();

            _context.Database.ExecuteSqlRaw("DELETE FROM FilmLicnost WHERE LicnostId = " + filmovi.LicnostId);

            foreach (int filmId in filmovi.Filmovi)
            {
                var film = _context.Film.Find(filmId);
                if (film != null)
                {
                    _context.FilmLicnost.Add(new FilmLicnost
                    {
                        FilmId = filmId,
                        LicnostId = filmovi.LicnostId,
                        Film = film,
                        Licnost = licnost
                    });
                }
            }

            _context.SaveChanges();

            return licnost;
        }

        public Licnost? Delete(int id)
        {
            var licnost = _context.Licnost.Find(id);
            if (licnost == null)
                return null;

            _context.Licnost.Remove(licnost);
            _context.SaveChanges();

            return licnost;
        }

        public List<Licnost> GetAll()
        {
            return _context.Licnost.Include(x => x.Filmovi).ThenInclude(x => x.Film).ToList();
        }

        public Licnost? GetOne(int id)
        {
            return _context.Licnost
                .Include(x => x.Filmovi)
                .ThenInclude(x => x.Film)
                .FirstOrDefault(x => x.Id == id);
        }

        public Licnost Save(LicnostWithoutFilm licnostViewModel)
        {
            var licnost = _context.Licnost.Find(licnostViewModel.Id);
            if (licnost == null)
            {
                licnost = new Licnost
                {
                    ImePrezime = licnostViewModel.ImePrezime,
                    IsRedatelj = licnostViewModel.IsRedatelj,
                    IsGlumac = licnostViewModel.IsGlumac
                };
                _context.Licnost.Add(licnost);
            }
            else
            {
                licnost.ImePrezime = licnostViewModel.ImePrezime;
                licnost.IsGlumac = licnostViewModel.IsGlumac;
                licnost.IsRedatelj = licnostViewModel.IsRedatelj;
            }

            _context.SaveChanges();
            return licnost;
        }
    }
}
