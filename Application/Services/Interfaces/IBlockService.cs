//using ProjectManagementApp.Api.Data.Entities;
using Application.DTOs;
using Core;
using Infrastructure.Data.Entities;

namespace Application.Services.Interfaces
{
    public interface IBlockService
    {
        //void Add<T>(T entity) where T : class;
        //void Delete<T>(T entity) where T : class;
       // Task<IEnumerable<Response<BlockDTO>> GetAllBlocksByBoardIdAsync(int boardId, bool includeCards = true);
        //Task<Block?> GetSingleBlockByBoardIdAsync(int boardId, int blockId, bool includeCards = false);
        //Task<bool> SaveChangesAsync();
        //Task UpdateBlockAsync(int id, Block block, bool includeCards = false);
     //   Task<Block?> CreateBlockAsync(int customerId, int boardId, Block newBlock);
        //Task<Block?> DeleteBlockAsync(int boardId, int blockId);
        Task<Response<CreateBlockDTO?>> CreateBlockAsync(int customerId, int boardId, CreateBlockDTO createBlockDTO);
        Task<Response<BlockDTO?>> GetSingleBlockByBoardIdAsync(int boardId, int blockId, bool includeCards = true);
     //   Task<BlockDTO?> GetSingleBlockByBoardIdAsync(int boardId, int blockId, bool includeCards = true);

        Task<IEnumerable<BlockDTO>> GetAllBlocksByBoardIdAsync(int boardId, bool includeCards = true);
        Task UpdateBlockAsync(int blockId, BlockDTO blockDTO, bool includeCards = false);
        Task<BlockDTO?> DeleteBlockAsync(int boardId, int blockId);





    }
}