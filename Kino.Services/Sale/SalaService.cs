using AutoMapper;
using Kino.Core.ViewModels;
using Kino.Repositories.Sale;

namespace Kino.Services.Sale
{
    public class SalaService(ISalaRepository salaRepository, IMapper mapper) : ISalaService
    {
        private readonly ISalaRepository _salaRepository = salaRepository;
        private readonly IMapper _mapper = mapper;

        public List<SalaViewModel> GetSale()
        {
            var sale = _salaRepository.GetAll();

            if (sale == null || sale.ToArray().Length == 0)
                return [];

            return _mapper.Map<List<SalaViewModel>>(sale);
        }

        public SalaViewModel? GetOneSala(int id)
        {
            var sala = _salaRepository.GetOne(id);

            if (sala == null)
                return null;

            return _mapper.Map<SalaViewModel>(sala);
        }

        public SalaViewModel? SaveSala(SalaViewModel salaViewModel)
        {
            var sala = _salaRepository.Save(salaViewModel);

            if (sala == null)
                return null;

            return _mapper.Map<SalaViewModel>(sala);
        }

        public SalaViewModel? DeleteSala(int id)
        {
            var sala = _salaRepository.Delete(id);

            if (sala == null)
                return null;

            return _mapper.Map<SalaViewModel>(sala);
        }
    }
}
