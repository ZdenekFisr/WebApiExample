using Application.Features.AmountToWords;
using Application.Features.FilmDatabase;
using Application.Features.RailVehicles.ListModel;
using Application.Features.RailVehicles.Model;
using AutoMapper;
using Domain;
using Domain.Entities;

namespace Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Film, FilmModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => Constants.FilmImageBaseUrl + src.ImagePath));
            CreateMap<FilmModel, Film>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImageUrl.Replace(Constants.FilmImageBaseUrl, string.Empty)));

            CreateMap<RailVehicle, RailVehiclePulledModel>();
            CreateMap<RailVehicle, RailVehicleDependentModel>()
                .ForMember(dest => dest.Efficiency, opt => opt.MapFrom(src => src.EfficiencyDependent));
            CreateMap<RailVehicle, RailVehicleIndependentModel>()
                .ForMember(dest => dest.Efficiency, opt => opt.MapFrom(src => src.EfficiencyIndependent));
            CreateMap<RailVehicle, RailVehicleHybridModel>();
            CreateMap<RailVehiclePulledModel, RailVehicle>();
            CreateMap<RailVehicleDependentModel, RailVehicle>()
                .ForMember(dest => dest.EfficiencyDependent, opt => opt.MapFrom(src => src.Efficiency));
            CreateMap<RailVehicleIndependentModel, RailVehicle>()
                .ForMember(dest => dest.EfficiencyIndependent, opt => opt.MapFrom(src => src.Efficiency));
            CreateMap<RailVehicleHybridModel, RailVehicle>();
            CreateMap<TractionDiagramPoint, TractionDiagramPointModel>();
            CreateMap<TractionDiagramPointModel, TractionDiagramPoint>();

            CreateMap<RailVehicle, RailVehicleListModel>();

            CreateMap<CurrencyCzechName, CurrencyCzechNameModel>();
        }
    }
}
