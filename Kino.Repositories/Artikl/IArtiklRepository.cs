using Kino.Core.ViewModels;
//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Kino.Repositories.Artikl
{
    public interface IArtiklRepository
    {
        List<ArtiklViewModel> GetArtikle(int pageNumber, int pageSize, string searchType, string searchQuery);
        List<Core.Entities.Artikl> GetAll();
        Core.Entities.Artikl GetOne(int id);
        Core.Entities.Artikl Save(ArtiklViewModel artiklViewModel);
        Core.Entities.Artikl Delete(int id);
        int GetArtiklCount();
    }
}
