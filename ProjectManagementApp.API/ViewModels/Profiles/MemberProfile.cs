using Application.DTOs;
using AutoMapper;
using Core;
using Infrastructure.Data.Entities;
using ProjectManagementApp.Api.ViewModels;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<MemberDTO, MemberViewModel>().ReverseMap();

            CreateMap<CreateMemberDTO, CreateMemberViewModel>().ReverseMap();

            CreateMap<Response<MemberDTO>, Response<MemberViewModel>>()
              .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));


        }
    }
}
