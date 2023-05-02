using Application.DTOs;
using AutoMapper;
using Core;
using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {

            CreateMap<Response<CardDTO>, Response<CardViewModel>>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));


            CreateMap<CardDTO, CardViewModel>().ReverseMap();
            CreateMap<CreateCardDTO, CreateCardViewModel>().ReverseMap();

        }
    }
}
