using Application.DTOs;
using AutoMapper;
using Core;
using Infrastructure.Data.Entities;
//using ProjectManagementApp.Api.ViewModels;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class BlockProfile : Profile
    {
        public BlockProfile()
        {
            CreateMap<BlockDTO, BlockViewModel>().ReverseMap()
             .ForMember(dest => dest.Cards, opt => opt.MapFrom(src => src.Cards));

            CreateMap<CreateBlockDTO, CreateBlockViewModel>().ReverseMap();


            CreateMap<Response<BlockDTO>, Response<BlockViewModel>>();
            // .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));




        }
    }
}
