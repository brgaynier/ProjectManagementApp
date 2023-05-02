using Application.DTOs;
using AutoMapper;
using Core;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {

            CreateMap<Response<WorkflowDTO>, Response<WorkflowViewModel>>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<WorkflowDTO, WorkflowViewModel>().ReverseMap();
            CreateMap<CreateWorkflowDTO, CreateWorkflowViewModel>().ReverseMap();

        }
    }
}