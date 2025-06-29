using AutoMapper;
using Kino.Core.ViewModels;
using Kino.Repositories.Licnosti;

namespace Kino.Services.Licnosti
{
    public class LicnostService(ILicnostRepository licnostRepository, IMapper mapper) : ILicnostService
    {
        private readonly ILicnostRepository _licnostRepository = licnostRepository;
        private readonly IMapper _mapper = mapper;

        public LicnostViewModel? AddFilm(LicnostFilmViewModel filmovi)
        {
            var result = _licnostRepository.AddFilm(filmovi);

            return _mapper.Map<Core.Entities.Licnost, LicnostViewModel>(result);
        }

        public LicnostViewModel? DeleteLicnost(int id)
        {
            var licnost = _licnostRepository.Delete(id);
            if (licnost == null)
                return null;

            return _mapper.Map<Core.Entities.Licnost, LicnostViewModel>(licnost);
        }

        public List<LicnostViewModel> GetLicnosti()
        {
            var licnost = _licnostRepository.GetAll();
            if (licnost == null || licnost.ToArray().Length == 0)
                return [];

            return _mapper.Map<List<Core.Entities.Licnost>, List<LicnostViewModel>>(licnost);
        }

        public LicnostViewModel? GetOneLicnost(int id)
        {
            var licnost = _licnostRepository.GetOne(id);
            if (licnost == null)
                return null;

            return _mapper.Map<Core.Entities.Licnost, LicnostViewModel>(licnost);
        }

        public LicnostViewModel? SaveLicnost(LicnostWithoutFilm licnostViewModel)
        {
            var licnost = _licnostRepository.Save(licnostViewModel);
            if (licnost == null)
                return null;

            return _mapper.Map<Core.Entities.Licnost, LicnostViewModel>(licnost);
        }
    }
}
