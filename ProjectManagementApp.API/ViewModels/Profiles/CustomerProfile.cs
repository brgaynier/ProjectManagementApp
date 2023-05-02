using Application.DTOs;
using AutoMapper;
using Core;
using Infrastructure.Data.Entities;
using ProjectManagementApp.Api.ViewModels;
using System.Runtime.CompilerServices;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
 

            CreateMap<CustomerDTO, CustomerViewModel>().ReverseMap();

            CreateMap<CreateCustomerDTO, CreateCustomerViewModel>().ReverseMap();

            CreateMap<Response<CustomerDTO>, Response<CustomerViewModel>>()
              .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));





        }
    }
}
