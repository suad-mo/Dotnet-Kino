using Kino.Core.Entities;
using Kino.Core.ViewModels;

namespace Kino.Repositories.Sale
{
    public interface ISalaRepository
    {
        List<Sala> GetAll();
        Sala? GetOne(int id);
        Sala Save(SalaViewModel salaViewModel);
        Sala? Delete(int id);
    }
}
