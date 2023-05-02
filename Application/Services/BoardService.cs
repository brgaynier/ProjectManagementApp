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
    public class BoardService : IBoardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<BoardService> _logger;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public BoardService(ApplicationDbContext dbContext, ILogger<BoardService> logger, ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<BoardDTO>> GetAllBoardsByCustomerIdAsync(int customerId, bool includeBlocks = true)
        {
            _logger.LogInformation($"Getting Boards");

            IQueryable<Board> query = _dbContext.Boards;

            if (includeBlocks)
            {
                query = query
                .Include(t => t.Blocks)
                .ThenInclude(t => t.Cards);
                //.ThenInclude(t => t.Checklist)
                //.ThenInclude(t => t.ChecklistItems);
            }

            // Add Query
            query = query
                .Where(t => t.Customer.CustomerId == customerId);
            //       .OrderByDescending(t => t.CardId);

            var boardDTOs = await query.ProjectTo<BoardDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return boardDTOs;

        }


        public async Task<Response<BoardDTO?>> GetSingleBoardByCustomerIdAsync(int boardId, int customerId, bool includeBlocks = false)
        {
            _logger.LogInformation($"Getting individual Board");

            IQueryable<Board> query = _dbContext.Boards;

            if (query == null)
            {
                return new Response<BoardDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Board does not exist" } } };

            }

            if (includeBlocks)
            {
                query = query
                .Include(t => t.Blocks)
                .ThenInclude(t => t.Cards);
            }

            // Add Query
            query = query
                .Where(t => t.BoardId == boardId && t.Customer.CustomerId == customerId);

            var boardDTO = await query.ProjectTo<BoardDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<BoardDTO?> { Value = boardDTO, Status = ResponseStatus.Ok };
        }

        public async Task<IEnumerable<BoardDTO>> GetAllBoardsByDueDate(DateTime dateTime, bool includeBlocks = false) //TODO needs customerID
        {
            _logger.LogInformation($"Getting all Boards by Date");

            IQueryable<Board> query = _dbContext.Boards
                .Include(c => c.DueDate);

            if (includeBlocks)
            {
                query = query
                .Include(c => c.Blocks);
                //     .ThenInclude(t => t.Cards);
            }

            // Order It            
            query = query.OrderBy(c => c.DueDate);
            //   .Where(c => c.DueDate.Date == dateTime.Date);

            var boardDTOs = await query.ProjectTo<BoardDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return boardDTOs;

        }

        public async Task<Response<CreateBoardDTO?>> CreateBoardAsync(int customerId, CreateBoardDTO createBoardDTO)
        {
            var customerDTO = await _customerService.GetSingleCustomerAsync(customerId);
            var customer = _mapper.Map<Customer>(customerDTO);

            if (customer == null)
            {
                return new Response<CreateBoardDTO?> { Value = null, Status = ResponseStatus.NotFound, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "No Customer found" } } };
            }


            var newBoard = _mapper.Map<Board>(createBoardDTO);
            newBoard.CustomerId = customerId;

            await _dbContext.Boards.AddAsync(newBoard);
            await _dbContext.SaveChangesAsync();

            var boardDTO = _mapper.Map<CreateBoardDTO>(newBoard);

            return new Response<CreateBoardDTO?> { Value = boardDTO, Status = ResponseStatus.Ok };

        }

        public async Task UpdateBoardAsync(int boardId, BoardDTO boardDTO, bool includeBlocks = false)
        {
            _logger.LogInformation($"Updating Board {boardId}");

            var updateBoard = await _dbContext.Boards.FindAsync(boardId);
            if (updateBoard != null)
            {
                updateBoard.Title = boardDTO.Title;
                updateBoard.DueDate = boardDTO.DueDate;

                _mapper.Map(updateBoard, boardDTO);

                await _dbContext.SaveChangesAsync();

            }

        }

        public async Task<BoardDTO?> DeleteBoardAsync(int customerId, int boardId)
        {
            var board = await _dbContext.Boards.FindAsync(boardId);

            if (board != null)
            {
                _dbContext.Boards.Remove(board);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedBoardDTO = _mapper.Map<BoardDTO>(board);
            return deletedBoardDTO;

        }
    }
}
