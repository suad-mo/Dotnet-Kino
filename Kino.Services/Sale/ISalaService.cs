using Kino.Core.ViewModels;

namespace Kino.Services.Sale
{
    public interface ISalaService
    {
        List<SalaViewModel> GetSale();
        SalaViewModel? GetOneSala(int id);
        SalaViewModel? SaveSala(SalaViewModel sala);
        SalaViewModel? DeleteSala(int id);
    }
}
