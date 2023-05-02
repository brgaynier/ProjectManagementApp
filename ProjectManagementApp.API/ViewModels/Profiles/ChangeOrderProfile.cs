using Application.DTOs;
using AutoMapper;
using Core;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class ChangeOrderProfile : Profile
    {
        public ChangeOrderProfile()
        {

            CreateMap<Response<ChangeOrderDTO>, Response<ChangeOrderViewModel>>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));


            CreateMap<ChangeOrderDTO, ChangeOrderViewModel>().ReverseMap();
            CreateMap<CreateChangeOrderDTO, CreateChangeOrderViewModel>().ReverseMap();

        }
    }
}
