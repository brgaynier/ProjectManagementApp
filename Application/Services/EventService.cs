using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CardService> _logger;
        private readonly IMapper _mapper;
        public EventService(ApplicationDbContext dbContext, ILogger<CardService> logger, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            _logger.LogInformation($"Getting all Events");

        //    IQueryable<Event> query = _dbContext.Events;

            IQueryable<Card> cardQuery = _dbContext.Cards;
           
            var cardEventList = new List<EventDTO>();
            foreach (var cardEvent in cardQuery)
            {
                var card = new EventDTO()
                {
                    EventId = cardEvent.CardId,
           //              Start = cardEvent.DueDate,
           //               End = cardEvent.DueDate,
                    Description = cardEvent.Description
                };
                cardEventList.Add(card);
            }

            // Order It
            // query = query.OrderBy(c => c.Start);


          //  var eventDTOs = await cardQuery.ProjectTo<EventDTO>(_mapper.ConfigurationProvider).ToArrayAsync();

            return cardEventList;
        }

        //public async Task<Response<EventDTO?>> GetSingleEventAsync(int eventId)
        //{
        //    _logger.LogInformation($"Getting an event with Id number {eventId}");

        //    IQueryable<Event> query = _dbContext.Events;
        //    //.Include(c => c.Boards);

        //    if (query == null)
        //    {
        //        return new Response<EventDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Event does not exist" } } };

        //    }

        //    //if (includeBoards)
        //    //{
        //    //    query = query
        //    //    .Include(w => w.WorkFlows)
        //    //    .Include(a => a.ChangeOrders)
        //    //    .Include(c => c.Boards)
        //    //    .ThenInclude(m => m.Blocks)
        //    //    .ThenInclude(t => t.Cards);
        //    //}

        //    // Query It
        //    query = query.Where(c => c.EventId == eventId);

        //    var eventDTO = await query.ProjectTo<EventDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        //    return new Response<EventDTO?> { Value = eventDTO, Status = ResponseStatus.Ok };
        //}

        //public async Task<Response<CreateEventDTO?>> CreateEventAsync(CreateEventDTO createEventDTO)
        //{

        //    var newEvent = _mapper.Map<Event>(createEventDTO);
        //    //newCard.BlockId = blockId;

        //    await _dbContext.Events.AddAsync(newEvent);
        //    await _dbContext.SaveChangesAsync();

        //    var eventDTO = _mapper.Map<CreateEventDTO>(newEvent);

        //    return new Response<CreateEventDTO?> { Value = eventDTO, Status = ResponseStatus.Ok };

        //}

        //public async Task UpdateCustomerAsync(int eventId, EventDTO eventDTO)
        //{
        //    _logger.LogInformation($"Updating Event {eventId}");

        //    var updateEvent = await _dbContext.Events.FindAsync(eventId);
        //    if (updateEvent != null)
        //    {
        //        updateEvent.Title = eventDTO.Title;
        //        updateEvent.Description = eventDTO.Description;
        //        updateEvent.End = eventDTO.End;
        //        updateEvent.Start = eventDTO.Start;
        //        updateEvent.StartTime = eventDTO.StartTime;
        //        updateEvent.EndTime = eventDTO.EndTime;
        //        updateEvent.IsFullDayEvent = eventDTO.IsFullDayEvent;
        //        updateEvent.IsRecurring = eventDTO.IsRecurring;

        //        _mapper.Map(updateEvent, eventDTO);

        //        await _dbContext.SaveChangesAsync();

        //    }
        //}

        //public async Task<EventDTO?> DeleteEventAsync(int eventId)
        //{
        //    var event = await _dbContext.Events.FindAsync(eventId);

        //    if (event != null)
        //    {
        //        _dbContext.EventsRemove(event);
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    var deletedEventDTO = _mapper.Map<EventDTO>(event);
        //    return deletedEventDTO;

        //}

    }

}
