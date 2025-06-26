using Kino.Core.Entities;
using Kino.Core.ViewModels;
using Kino.Infrastructure.Database;

namespace Kino.Repositories.KategorijaArtikl
{
    public class KategorijaArtiklRepository
    {
        private readonly DatabaseContext _context;

        public KategorijaArtiklRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Core.Entities.KategorijaArtikl? Delete(int id)
        {
            var kategorijaArtikl = _context.KategorijaArtikl.Find(id);
            if (kategorijaArtikl == null)
                return null;

            _context.KategorijaArtikl.Remove(kategorijaArtikl);
            _context.SaveChanges();

            return kategorijaArtikl;
        }

        public List<Core.Entities.KategorijaArtikl> GetAll()
        {
            return _context.KategorijaArtikl.ToList();
        }

        public Core.Entities.KategorijaArtikl? GetOne(int id)
        {
            return _context.KategorijaArtikl.Find(id);
        }

        public Core.Entities.KategorijaArtikl Save(KategorijaArtiklViewModel kategorijaArtiklViewModel)
        {
            var kategorijaArtikl = _context.KategorijaArtikl.Find(kategorijaArtiklViewModel.Id);

            if (kategorijaArtikl == null)
            {
                kategorijaArtikl = new Core.Entities.KategorijaArtikl
                {
                    Naziv = kategorijaArtiklViewModel.Naziv
                };
                _context.KategorijaArtikl.Add(kategorijaArtikl);
            }
            else
            {
                kategorijaArtikl.Naziv = kategorijaArtiklViewModel.Naziv;
            }

            _context.SaveChanges();
            return kategorijaArtikl;
        }

    }
}
