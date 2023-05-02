using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
//using ProjectManagementApp.Api.Data;
//using ProjectManagementApp.Api.Data.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using Application.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Core;

namespace Application.Services
{
    public class BlockService : IBlockService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<BlockService> _logger;
        private readonly IBoardService _boardService;
        private readonly IMapper _mapper;

        public BlockService(ApplicationDbContext dbContext, ILogger<BlockService> logger, IBoardService boardService, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardService = boardService ?? throw new ArgumentNullException(nameof(boardService));
        }


        public async Task<IEnumerable<BlockDTO>> GetAllBlocksByBoardIdAsync(int boardId, bool includeCards = true)
        {
            _logger.LogInformation($"Getting all Blocks for a Board with Id {boardId}");

            IQueryable<Block> query = _dbContext.Blocks;

            if (includeCards)
            {
                query = query
                .Include(t => t.Cards);

            }

            // Add Query
            query = query
                .Where(t => t.Board.BoardId == boardId)
                    .OrderByDescending(t => t.BlockId);


            // return query;
           // return await query.ToArrayAsync();
            var blockDTOs = await query.ProjectTo<BlockDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return blockDTOs;


        }

        public async Task<Response<BlockDTO?>> GetSingleBlockByBoardIdAsync(int boardId, int blockId, bool includeCards = true)
        {
            _logger.LogInformation($"Getting single block with board id {boardId}");

            IQueryable<Block> query = _dbContext.Blocks;

            if (query == null)
            {
                return new Response<BlockDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Block does not exist" } } };

            }

            if (includeCards)
            {
                query = query
                    .Include(t => t.Cards);
            }

            // Add Query
            query = query
                .Where(t => t.BlockId == blockId && t.Board.BoardId == boardId);


            var blockDTO = await query.ProjectTo<BlockDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<BlockDTO?> { Value = blockDTO, Status = ResponseStatus.Ok };
        }

        public async Task<Response<CreateBlockDTO?>> CreateBlockAsync(int customerId, int boardId, CreateBlockDTO createBlockDTO)
        {  
            var boardDTO = await _boardService.GetSingleBoardByCustomerIdAsync(boardId, customerId);
            var board = _mapper.Map<Board>(boardDTO);

            if (board == null)
            {
                return new Response<CreateBlockDTO?> { Value = null, Status = ResponseStatus.NotFound, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "No board found" } } };
            }

            var newBlock = _mapper.Map<Block>(createBlockDTO);
            newBlock.BoardId = boardId;


            await _dbContext.Blocks.AddAsync(newBlock);
            await _dbContext.SaveChangesAsync();


            var blockDTO = _mapper.Map<CreateBlockDTO>(newBlock);
            return new Response<CreateBlockDTO?> { Value = blockDTO, Status = ResponseStatus.Ok };

        }

        public async Task UpdateBlockAsync(int blockId, BlockDTO blockDTO, bool includeCards = false)
        {
            _logger.LogInformation($"Updating Block {blockId}");

            var updateBlock = await _dbContext.Blocks.FindAsync(blockId);
            if (updateBlock != null)
            {
                updateBlock.BlockName = blockDTO.BlockName;

                _mapper.Map(updateBlock, blockDTO);

                await _dbContext.SaveChangesAsync();

            }

        }

        public async Task<BlockDTO?> DeleteBlockAsync(int boardId, int blockId)
        {

            var block = await _dbContext.Blocks.FindAsync(blockId);

            if (block != null)
            {
                _dbContext.Blocks.Remove(block);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedBlockDTO = _mapper.Map<BlockDTO>(block);
            return deletedBlockDTO;

        }
    }
}
