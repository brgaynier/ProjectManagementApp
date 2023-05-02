using Application.DTOs;
using AutoMapper;
using Core;
using Infrastructure.Data.Entities;
using ProjectManagementApp.Api.ViewModels;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
           // CreateMap<Board, BoardViewModel>();
            CreateMap<BoardDTO, BoardViewModel>().ReverseMap();

            CreateMap<Response<BoardDTO>, Response<BoardViewModel>>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));


            CreateMap<CreateBoardDTO, CreateBoardViewModel>().ReverseMap();
        }
    }
}
