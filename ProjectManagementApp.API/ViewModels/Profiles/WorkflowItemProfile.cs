using Application.DTOs;
using AutoMapper;
using Core;

namespace ProjectManagementApp.Api.ViewModels.Profiles
{
    public class WorkflowItemProfile : Profile
    {
        public WorkflowItemProfile()
        {

            CreateMap<Response<WorkflowItemDTO>, Response<WorkflowItemViewModel>>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));


            CreateMap<WorkflowItemDTO, WorkflowItemViewModel>().ReverseMap();
            CreateMap<CreateWorkflowItemDTO, CreateWorkflowItemViewModel>().ReverseMap();

        }
    }
}
