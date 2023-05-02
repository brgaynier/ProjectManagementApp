using Application.DTOs;
using Core;

namespace Application.Services.Interfaces
{
    public interface IEventService
    {
        //Task<Response<CreateEventDTO?>> CreateEventAsync(CreateEventDTO createEventDTO);
    //    Task<EventDTO?> DeleteEventAsync(int eventId);
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();
        //Task<Response<EventDTO?>> GetSingleEventAsync(int eventId);
        //Task UpdateCustomerAsync(int eventId, EventDTO eventDTO);
    }
}