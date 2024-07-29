using AutoMapper;
using WebApiExample.Features.FilmDatabase;
using WebApiExample.Features.RailVehicles;

namespace WebApiExample
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Film, FilmModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => Constants.FilmImageBaseUrl + src.ImagePath));
            CreateMap<FilmModel, Film>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImageUrl.Replace(Constants.FilmImageBaseUrl, string.Empty)));

            CreateMap<RailVehicle, RailVehicleModel>();
            CreateMap<RailVehicleModel, RailVehicle>();
        }
    }
}
