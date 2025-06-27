using Kino.Core.Entities;
using Kino.Core.ViewModels;
using Kino.Infrastructure.Database;

namespace Kino.Repositories.Sale
{
    public class SalaRepository : ISalaRepository
    {
        private readonly DatabaseContext _context;

        public SalaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Sala> GetAll()
        {
            return _context.Sala.ToList();
        }

        public Sala? GetOne(int id)
        {
            return _context.Sala.Find(id);
        }

        public Sala Save(SalaViewModel salaViewModel)
        {
            var sala = _context.Sala.Find(salaViewModel.Id);
            if (sala == null)
            {
                sala = new Sala
                {
                    Naziv = salaViewModel.Naziv,
                    Kapacitet = salaViewModel.Kapacitet
                };

                _context.Sala.Add(sala);
            }
            else
            {
                sala.Naziv = salaViewModel.Naziv;
                sala.Kapacitet = salaViewModel.Kapacitet;
            }

            _context.SaveChanges();
            return sala;
        }

        public Sala? Delete(int id)
        {
            var sala = _context.Sala.Find(id);
            if (sala == null)
                return null;

            _context.Sala.Remove(sala);
            _context.SaveChanges();

            return sala;
        }
    }
}
