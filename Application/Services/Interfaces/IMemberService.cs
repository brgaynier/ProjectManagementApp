//using ProjectManagementApp.Api.Data.Entities;
using Application.DTOs;
using Core;
using Infrastructure.Data.Entities;


namespace Application.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDTO>> GetAllMembersAsync(); //include customers?
        Task<Response<MemberDTO?>> GetSingleMemberAsync(int memberId);
        Task<Response<CreateMemberDTO?>> CreateMemberAsync(CreateMemberDTO createMemberDTO);
        Task UpdateMemberAsync(int memberId, MemberDTO memberDTO);
        Task<MemberDTO?> DeleteMemberAsync(int memberId);

    }
}