using Kino.Core.ViewModels;

namespace Kino.Services.KategorijaArtikl
{
    public interface IKategorijaArtiklService
    {
        List<KategorijaArtiklViewModel> GetKategorije();
        KategorijaArtiklViewModel? GetOneKategorija(int id);
        KategorijaArtiklViewModel? Save(KategorijaArtiklViewModel kategorijaViewModel);
        KategorijaArtiklViewModel? Delete(int id);
    }
}
