using Kino.Core.Pagination;
using Kino.Core.ViewModels;

namespace Kino.Services.Projekcije
{
    public interface IProjekcijaService
    {
        List<ProjekcijaViewModel> GetProjekcije(int pageNumber, int pageSize, string search_query, string search_type);
        ProjekcijaViewModel? GetProjekcija(int id);
        ProjekcijaViewModel? SaveProjekcija(ProjekcijaViewModel projekcija);
        PaginationMetadata GetPaginationMetadata(int pageSize);
        List<ProjekcijaViewModel> GetAllProjekcijeForMovie(int filmId, string language);

    }
}
