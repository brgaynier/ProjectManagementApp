using Application.Services.Interfaces;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
//using ProjectManagementApp.Api.Data;
//using ProjectManagementApp.Api.Data.Entities;
using System.Net.Sockets;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using Core;
using System.Net.Http;
using Application.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CardService> _logger;
      //  private readonly IMemberService _memberService;
        private readonly IBlockService _blockService;
        private readonly IMapper _mapper;
        

        public CardService(ApplicationDbContext dbContext, ILogger<CardService> logger, IBlockService blockService, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
          //  _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            _blockService = blockService ?? throw new ArgumentNullException(nameof(blockService));
        }


        public async Task<IEnumerable<CardDTO>> GetAllCardsByBlockIdAsync(int blockId, bool includeChecklist = false)
        {
            _logger.LogInformation($"Getting Cards");

            IQueryable<Card> query = _dbContext.Cards;

            if (includeChecklist)
            {
                query = query
                .Include(t => t.Checklist)
                .ThenInclude(t => t.ChecklistItems);
            }

            // Add Query
            query = query
                .Where(t => t.Block.BlockId == blockId)
                    .OrderByDescending(t => t.CardId);
            //    .OrderByDescending(t => t.Id);  //Is this blockId or CardId?

            var cardDTOs = await query.ProjectTo<CardDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return cardDTOs;

        }

       public async Task<Response<CardDTO?>> GetSingleCardByBlockIdAsync(int cardId, int blockId, bool includeChecklist = false)
        {
            _logger.LogInformation($"Getting individual Card");

            IQueryable<Card> query = _dbContext.Cards;

            if (query == null)
            {
                return new Response<CardDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Card does not exist" } } };

            }


            if (includeChecklist)
            {
                query = query
                .Include(t => t.Checklist)
                .ThenInclude(t => t.ChecklistItems);
            }

            // Add Query
            query = query
                .Where(t => t.CardId == cardId && t.Block.BlockId == blockId);

            var cardDTO = await query.ProjectTo<CardDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<CardDTO?> { Value = cardDTO, Status = ResponseStatus.Ok };
        }

        public async Task<Response<CreateCardDTO?>> CreateCardAsync(int boardId, int blockId, CreateCardDTO createCardDTO)
        {   //use DTO to access database.. front end doesn't need to know what the DB looks like
            //validations first, then actions, then return

            var blockDTO = await _blockService.GetSingleBlockByBoardIdAsync(boardId, blockId);
            var block = _mapper.Map<Block>(blockDTO);

            if (block == null)
            {
                return new Response<CreateCardDTO?> { Value = null, Status = ResponseStatus.NotFound, ErrorMessages = new List<ResponseMessage> { new ResponseMessage {DisplayText = "No Block found" } } };
            }

            var newCard = _mapper.Map<Card>(createCardDTO);
            newCard.BlockId = blockId;

            await _dbContext.Cards.AddAsync(newCard);
            await _dbContext.SaveChangesAsync();

            var cardDTO = _mapper.Map<CreateCardDTO>(newCard);

            return new Response<CreateCardDTO?> { Value = cardDTO, Status = ResponseStatus.Ok };

        }


        public async Task UpdateCardAsync(int cardId, CardDTO cardDTO)  //TODO add other card properties
        {

            _logger.LogInformation($"Updating Card {cardId}");

            var updateCard = await _dbContext.Cards.FindAsync(cardId);
            if (updateCard != null)
            {
                updateCard.Title = cardDTO.Title;
                updateCard.Description = cardDTO.Description;
                updateCard.Comment = cardDTO.Comment;
            //    updateCard.DueDate = cardDTO.DueDate;

                _mapper.Map(updateCard, cardDTO);

                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<CardDTO?> DeleteCardAsync(int blockId, int cardId)
        {
            var card = await _dbContext.Cards.FindAsync(cardId);

            if (card != null)
            {
                    _dbContext.Cards.Remove(card);
                   await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedCardDTO = _mapper.Map<CardDTO>(card);
            return deletedCardDTO;

        }

    }
}
