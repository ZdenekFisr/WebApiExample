using Application.Features.AmountToWords;
using Application.Features.FilmDatabase;
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

            CreateMap<ElectrificationType, ElectrificationTypeModel>();
            CreateMap<ElectrificationTypeModel, ElectrificationType>();
            CreateMap<ElectrificationType, ElectrificationTypeListModel>();

            CreateMap<RailVehicle, RailVehiclePulledModel>();
            CreateMap<RailVehicle, RailVehicleDrivingModel>();
            CreateMap<RailVehiclePulledModel, RailVehicle>();
            CreateMap<RailVehicleDrivingModel, RailVehicle>();
            CreateMap<VehicleTractionSystem, VehicleTractionSystemModel>();
            CreateMap<VehicleTractionSystemModel, VehicleTractionSystem>();
            CreateMap<TractionDiagramPoint, TractionDiagramPointModel>();
            CreateMap<TractionDiagramPointModel, TractionDiagramPoint>();

            CreateMap<RailVehicle, RailVehiclePulledListModel>();
            CreateMap<RailVehicle, RailVehicleDrivingListModel>()
                .ForMember(dest => dest.Performance, opt => opt.MapFrom(src => src.TractionSystems.Max(vts => vts.Performance)))
                .ForMember(dest => dest.MaxPullForce, opt => opt.MapFrom(src => src.TractionSystems.Max(vts => vts.MaxPullForce)));
            CreateMap<RailVehicle, RailVehicleDeletedModel>();

            CreateMap<Train, TrainOutputModel>();
            CreateMap<TrainInputModel, Train>();
            CreateMap<TrainVehicle, TrainVehicleOutputModel>();
            CreateMap<TrainVehicleInputModel, TrainVehicle>();

            CreateMap<Train, TrainListModel>();
            CreateMap<Train, TrainDeletedModel>();

            CreateMap<CurrencyCzechName, CurrencyCzechNameModel>();
        }
    }
}
