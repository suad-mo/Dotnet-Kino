using AutoMapper;
using Kino.Core.ViewModels;
using Kino.Repositories.KategorijaArtikl;

namespace Kino.Services.KategorijaArtikl
{
    public class KategorijaArtiklService : IKategorijaArtiklService
    {
        private readonly IKategorijaArtiklRepository _kategorijaArtiklRepository;
        private readonly IMapper _mapper;

        public KategorijaArtiklService(IKategorijaArtiklRepository kategorijaArtiklRepository, IMapper mapper)
        {
            _kategorijaArtiklRepository = kategorijaArtiklRepository;
            _mapper = mapper;
        }
        public KategorijaArtiklViewModel? Delete(int id)
        {
            var kategorijaArtikl = _kategorijaArtiklRepository.Delete(id);
            if (kategorijaArtikl == null)
                return null;

            return _mapper.Map<KategorijaArtiklViewModel>(kategorijaArtikl);
        }

        public List<KategorijaArtiklViewModel> GetKategorije()
        {
            var kategorijaArtikl = _kategorijaArtiklRepository.GetAll();
            if (kategorijaArtikl == null || kategorijaArtikl.ToArray().Length == 0)
                return new List<KategorijaArtiklViewModel>();

            return _mapper.Map<List<KategorijaArtiklViewModel>>(kategorijaArtikl);
        }

        public KategorijaArtiklViewModel? GetOneKategorija(int id)
        {
            var kategorijaArtikl = _kategorijaArtiklRepository.GetOne(id);
            if (kategorijaArtikl == null)
                return null;

            return _mapper.Map<KategorijaArtiklViewModel>(kategorijaArtikl);
        }

        public KategorijaArtiklViewModel? Save(KategorijaArtiklViewModel kategorijaViewModel)
        {
            var kategorijaArtikl = _kategorijaArtiklRepository.Save(kategorijaViewModel);
            if (kategorijaArtikl == null)
                return null;

            return _mapper.Map<KategorijaArtiklViewModel>(kategorijaArtikl);
        }
    }
}
