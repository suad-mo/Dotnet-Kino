using Kino.Core.Entities;
using Kino.Core.ViewModels;

namespace Kino.Repositories.Licnosti
{
    public interface ILicnostRepository
    {
        List<Licnost> GetAll();
        Licnost? GetOne(int id);
        Licnost Save(LicnostWithoutFilm licnostViewModel);
        Licnost? Delete(int id);
        Licnost? AddFilm(LicnostFilmViewModel filmovi);
    }
}
