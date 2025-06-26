using Kino.Core.ViewModels;

namespace Kino.Repositories.KategorijaArtikl
{
    public interface IKategorijaArtiklRepository
    {
        List<Core.Entities.KategorijaArtikl> GetAll();
        Core.Entities.KategorijaArtikl? GetOne(int id);
        Core.Entities.KategorijaArtikl Save(KategorijaArtiklViewModel kategorijaArtiklViewModel);
        Core.Entities.KategorijaArtikl? Delete(int id);
    }
}
