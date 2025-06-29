using AutoMapper;
using Kino.Core.Pagination;
using Kino.Core.ViewModels;
using Kino.Repositories.Projekcije;

namespace Kino.Services.Projekcije
{
    public class ProjekcijaService(IProjekcijaRepository projekcijaRepository, IMapper mapper) : IProjekcijaService
    {
        private readonly IProjekcijaRepository _projekcijaRepository = projekcijaRepository;
        private readonly IMapper _mapper = mapper;

        public List<ProjekcijaViewModel> GetProjekcije(int pageNumber, int pageSize, string search_query, string search_type)
        {
            var result = _projekcijaRepository.GetProjekcije(pageNumber, pageSize, search_query, search_type);
            if (result == null || result.ToArray().Length == 0)
                return [];

            return result;
        }

        public ProjekcijaViewModel? GetProjekcija(int id)
        {
            var result = _projekcijaRepository.GetProjekcija(id);

            if (result == null)
                return null;

            return _mapper.Map<ProjekcijaViewModel>(result);
        }

        public ProjekcijaViewModel? SaveProjekcija(ProjekcijaViewModel projekcija)
        {
            var result = _projekcijaRepository.SaveProjekcija(projekcija);

            if (result == null)
                return null;

            return _mapper.Map<ProjekcijaViewModel>(result);
        }

        public PaginationMetadata GetPaginationMetadata(int pageSize)
        {
            var count = _projekcijaRepository.GetProjekcijeCount();

            var pageCount = (int)Math.Ceiling(count / (double)pageSize);

            return new PaginationMetadata(count, pageCount);
        }

        public List<ProjekcijaViewModel> GetAllProjekcijeForMovie(int filmId, string language)
        {
            var result = _projekcijaRepository.GetAllProjekcijeForMovie(filmId, language);

            if (result == null)
                return [];

            return _mapper.Map<List<ProjekcijaViewModel>>(result);
        }

    }
}
