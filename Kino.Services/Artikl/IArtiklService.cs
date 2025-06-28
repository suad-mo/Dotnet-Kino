using Kino.Core.Pagination;
using Kino.Core.ViewModels;

namespace Kino.Services.Artikl
{
    public interface IArtiklService
    {
        List<ArtiklViewModel> GetAllArtikle(int pageNumber, int pageSize, string searchType, string searchQuery);
        List<ArtiklViewModel> GetArtikle();
        ArtiklViewModel GetOneArtikl(int id);
        ArtiklViewModel Save(ArtiklViewModel artiklViewModel);
        ArtiklViewModel Delete(int id);
        PaginationMetadata GetPaginationMetadata(int pageSize);
    }
}
