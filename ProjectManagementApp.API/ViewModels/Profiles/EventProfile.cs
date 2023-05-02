using Application.DTOs;
using AutoMapper;
using Core;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class EventProfile : Profile 
    {
        public EventProfile()
        {
            CreateMap<EventDTO, EventViewModel>().ReverseMap();

            CreateMap<Response<EventDTO>, Response<EventViewModel>>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));


            CreateMap<CreateEventDTO, CreateEventViewModel>().ReverseMap();
        }
    }
}
