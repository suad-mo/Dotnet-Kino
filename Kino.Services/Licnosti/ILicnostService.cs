using Kino.Core.ViewModels;

namespace Kino.Services.Licnosti
{
    public interface ILicnostService
    {
        List<LicnostViewModel> GetLicnosti();
        LicnostViewModel? GetOneLicnost(int id);
        LicnostViewModel? SaveLicnost(LicnostWithoutFilm licnostViewModel);
        LicnostViewModel? DeleteLicnost(int id);
        LicnostViewModel? AddFilm(LicnostFilmViewModel filmovi);
    }
}
