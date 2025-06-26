using AutoMapper;
using Kino.Core.Pagination;
using Kino.Core.ViewModels;
using Kino.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Kino.Repositories.Artikl
{
    public class ArtiklRepository : IArtiklRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        private int artiklCount;

        public ArtiklRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Core.Entities.Artikl? Delete(int id)
        {
            var artikl = _context.Artikl.Find(id);
            if (artikl == null)
                return null;
            
            _context.Artikl.Remove(artikl);
            _context.SaveChanges();
            
            return artikl;
        }

        /// <summary>
        /// Vraća listu svih artikala iz baze, uključujući povezanu kategoriju za svaki artikl.
        /// Koristi eager loading (Include) kako bi se izbegao dodatni upit za svaku kategoriju.
        /// </summary>
        public List<Core.Entities.Artikl> GetAll()
        {
            return _context.Artikl.Include(x => x.KategorijaArtikl).ToList();
        }

        public Core.Entities.Artikl? GetOne(int id)
        {
            return _context.Artikl.Include(x => x.KategorijaArtikl).FirstOrDefault(x => x.Id == id);
        }

        public Core.Entities.Artikl? Save(ArtiklViewModel artiklViewModel)
        {
            var kategorijaArtikl = _context.KategorijaArtikl.Find(artiklViewModel.KategorijaId);

            if (kategorijaArtikl == null)
                return null;

            var artikl = _context.Artikl.Find(artiklViewModel.Id);
            if (artikl == null)
            {
                artikl = new Core.Entities.Artikl
                {
                    Naziv = artiklViewModel.Naziv,
                    Cijena = artiklViewModel.Cijena,
                    KolicinaNaStanju = artiklViewModel.KolicinaNaStanju,
                    KategorijaId = artiklViewModel.KategorijaId,
                    KategorijaArtikl = kategorijaArtikl // Fix: Set the required member 'KategorijaArtikl'
                };
                _context.Artikl.Add(artikl);
            }
            else
            {
                artikl.Naziv = artiklViewModel.Naziv;
                artikl.Cijena = artiklViewModel.Cijena;
                artikl.KolicinaNaStanju = artiklViewModel.KolicinaNaStanju;
                artikl.KategorijaId = artiklViewModel.KategorijaId;
                artikl.KategorijaArtikl = kategorijaArtikl; // Fix: Ensure 'KategorijaArtikl' is updated
            }

            _context.SaveChanges();
            return artikl;
        }

        public List<ArtiklViewModel> GetArtikle(int pageNumber, int pageSize, string searchType, string searchQuery)
        {
            IQueryable<ArtiklViewModel> result;

            switch (searchType)
            {
                case "artikl":
                    result = _context.Artikl.Include(x => x.KategorijaArtikl).Where(a => searchQuery == null || searchQuery == "" || a.Naziv.ToLower().StartsWith(searchQuery.ToLower())).Select(a => _mapper.Map<ArtiklViewModel>(a));
                    break;

                case "id":
                    int parsedQueryValue = 0;
                    bool canParse = searchQuery != null && searchQuery != "" && int.TryParse(searchQuery, out parsedQueryValue);
                    if (canParse && parsedQueryValue != 0)
                        result = _context.Artikl.Include(x => x.KategorijaArtikl).Where(a => a.Id.Equals(parsedQueryValue))
                                              .Select(a => _mapper.Map<ArtiklViewModel>(a));
                    else
                        result = _context.Artikl.Include(x => x.KategorijaArtikl).Select(a => _mapper.Map<ArtiklViewModel>(a));
                    break;

                default:
                    result = _context.Artikl.Include(x => x.KategorijaArtikl).Select(a => _mapper.Map<ArtiklViewModel>(a));
                    break;
            }

            this.artiklCount = result.Count();
            return PaginatedList<ArtiklViewModel>.Create(result.AsNoTracking(), pageNumber, pageSize);
        }

        public int GetArtiklCount()
        {
            return artiklCount;
        }
    }
}
