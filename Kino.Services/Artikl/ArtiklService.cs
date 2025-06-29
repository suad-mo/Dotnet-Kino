using AutoMapper;
using Kino.Core.Pagination;
using Kino.Core.ViewModels;
using Kino.Repositories.Artikl;

namespace Kino.Services.Artikl
{
    public class ArtiklService(IArtiklRepository artiklRepository, IMapper mapper) : IArtiklService
    {
        private readonly IArtiklRepository _artiklRepository = artiklRepository;
        private readonly IMapper _mapper = mapper;

        public ArtiklViewModel? Delete(int id)
        {
            var artikl = _artiklRepository.Delete(id);
            if (artikl == null)
                return null;

            return _mapper.Map<ArtiklViewModel>(artikl);
        }

        public List<ArtiklViewModel> GetArtikle()
        {
            var artikl = _artiklRepository.GetAll();
            if (artikl == null || artikl.ToArray().Length == 0)
              return [];

            return _mapper.Map<List<ArtiklViewModel>>(artikl);
        }

        public ArtiklViewModel? GetOneArtikl(int id)
        {
            var artikl = _artiklRepository.GetOne(id);
            if (artikl == null)
                return null;

            return _mapper.Map<ArtiklViewModel>(artikl);
        }

        public ArtiklViewModel? Save(ArtiklViewModel artiklViewModel)
        {
            var artikl = _artiklRepository.Save(artiklViewModel);
            if (artikl == null)
                return null;

            return _mapper.Map<ArtiklViewModel>(artikl);
        }

        public List<ArtiklViewModel> GetAllArtikle(int pageNumber, int pageSize, string searchType, string searchQuery)
        {
            var artiklList = _artiklRepository.GetArtikle(pageNumber, pageSize, searchType, searchQuery);

            return artiklList;
        }

        public PaginationMetadata GetPaginationMetadata(int pageSize)
        {
            var count = _artiklRepository.GetArtiklCount();

            var pageCount = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginationMetadata(count, pageCount);
        }
    }
}
