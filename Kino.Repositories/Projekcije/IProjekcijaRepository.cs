using Kino.Core.Entities;
using Kino.Core.ViewModels;

namespace Kino.Repositories.Projekcije
{
    public interface IProjekcijaRepository
    {
        List<ProjekcijaViewModel> GetProjekcije(int pageNumber, int pageSize, string search_query, string search_type);
        Projekcija? GetProjekcija(int id);
        Projekcija? SaveProjekcija(ProjekcijaViewModel projekcija);
        int GetProjekcijeCount();
        List<Projekcija> GetAllProjekcijeForMovie(int filmId, string language);
    }
}
