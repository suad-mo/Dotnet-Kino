using AutoMapper;
using Kino.Core.Entities;
using Kino.Core.ViewModels;

namespace Kino.Infrastructure.AutoMaper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<FilmLicnost, LicnostViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Licnost.Id))
                .ForMember(dest => dest.ImePrezime, opt => opt.MapFrom(src => src.Licnost.ImePrezime))
                .ForMember(dest => dest.IsGlumac, opt => opt.MapFrom(src => src.Licnost.IsGlumac))
                .ForMember(dest => dest.IsRedatelj, opt => opt.MapFrom(src => src.Licnost.IsRedatelj))
                .ForMember(dest => dest.Filmovi, opt => opt.Ignore());

            CreateMap<FilmLicnost, FilmViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Film.Id))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Film.IsActive))
                .ForMember(dest => dest.Jezik, opt => opt.MapFrom(src => src.Film.Jezik))
                .ForMember(dest => dest.Naziv, opt => opt.MapFrom(src => src.Film.Naziv))
                .ForMember(dest => dest.Opis, opt => opt.MapFrom(src => src.Film.Opis))
                .ForMember(dest => dest.Trajanje, opt => opt.MapFrom(src => src.Film.Trajanje))
                .ForMember(dest => dest.UrlFotografije, opt => opt.MapFrom(src => src.Film.UrlFotografije))
                .ForMember(dest => dest.Godina, opt => opt.MapFrom(src => src.Film.Godina))
                .ForMember(dest => dest.Licnosti, opt => opt.Ignore());

            CreateMap<Film, FilmViewModel>();
            CreateMap<FilmViewModel, Film>();
            CreateMap<Sala, SalaViewModel>();
            CreateMap<SalaViewModel, Sala>();
            CreateMap<Projekcija, ProjekcijaViewModel>();
            CreateMap<ProjekcijaViewModel, Projekcija>();
            CreateMap<UserAccount, UpdateUserViewModel>();
            CreateMap<Licnost, LicnostViewModel>();
            CreateMap<LicnostViewModel, Licnost>();
            CreateMap<KategorijaArtikl, KategorijaArtiklViewModel>();
            CreateMap<KategorijaArtiklViewModel, KategorijaArtikl>();
            CreateMap<Artikl, ArtiklViewModel>();
            CreateMap<ArtiklViewModel, Artikl>();
        }
    }
}
