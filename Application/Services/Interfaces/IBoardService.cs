//using ProjectManagementApp.Api.Data.Entities;
using Application.DTOs;
using Core;
using Infrastructure.Data.Entities;

namespace Application.Services.Interfaces
{
    public interface IBoardService
    {
      
        Task<IEnumerable<BoardDTO>> GetAllBoardsByCustomerIdAsync(int customerId, bool includeBlocks = true);
        Task<IEnumerable<BoardDTO>> GetAllBoardsByDueDate(DateTime dateTime, bool includeBlocks = false);
        Task<Response<BoardDTO?>> GetSingleBoardByCustomerIdAsync(int boardId, int customerId, bool includeBlocks = false);
        Task UpdateBoardAsync(int boardId, BoardDTO boardDTO, bool includeBlocks = false);
        Task<Response<CreateBoardDTO?>> CreateBoardAsync(int customerId, CreateBoardDTO createBoardDTO);
        Task<BoardDTO?> DeleteBoardAsync(int customerId, int boardId);


    }
}