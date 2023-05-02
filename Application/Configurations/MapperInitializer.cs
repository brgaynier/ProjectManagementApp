using Application.DTOs;
using AutoMapper;
using Core;
using Infrastructure.Data.Entities;

namespace Application.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Card, CreateCardDTO>().ReverseMap();
            CreateMap<Card, Response<CardDTO>>().ReverseMap();

            CreateMap<Block, BlockDTO>().ReverseMap();
            CreateMap<Block, Response<BlockDTO>>().ReverseMap();
            CreateMap<Block, CreateBlockDTO>().ReverseMap();

            CreateMap<Board, BoardDTO>().ReverseMap();
            CreateMap<Board, CreateBoardDTO>().ReverseMap();
            CreateMap<Board, Response<BoardDTO>>().ReverseMap();


            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<Customer, Response<CustomerDTO>>().ReverseMap();

            CreateMap<ChangeOrder, ChangeOrderDTO>().ReverseMap();
            CreateMap<ChangeOrder, CreateChangeOrderDTO>().ReverseMap();
            CreateMap<ChangeOrder, Response<ChangeOrderDTO>>().ReverseMap();

            CreateMap<WorkFlow, WorkflowDTO>().ReverseMap();
            CreateMap<WorkFlow, CreateWorkflowDTO>().ReverseMap();
            CreateMap<WorkFlow, Response<WorkflowDTO>>().ReverseMap();

            CreateMap<WorkFlowItem, WorkflowItemDTO>().ReverseMap();
            CreateMap<WorkFlowItem, CreateWorkflowItemDTO>().ReverseMap();
            CreateMap<WorkFlowItem, Response<WorkflowItemDTO>>().ReverseMap();

            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, CreateEventDTO>().ReverseMap();
            CreateMap<Event, Response<EventDTO>>().ReverseMap();
            CreateMap<Card, EventDTO>().ReverseMap();


            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<Member, CreateMemberDTO>().ReverseMap();
            CreateMap<Member, Response<MemberDTO>>().ReverseMap();


        }
    }
}
