using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Api.ViewModels;
using Infrastructure.Data.Entities;
using Application.Services.Interfaces;
using Newtonsoft.Json;  //maybe?
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //   [Authorize]  //can be used on individual controllers or registered as a global filter in startup
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _eventService = eventService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EventViewModel>> Get()
        {
            try
            {
                var eventsDTO = await _eventService.GetAllEventsAsync();
                var eventsViewModel = _mapper.Map<EventViewModel[]>(eventsDTO);

                return Ok(eventsViewModel);

                //      return Ok(results);

            }
            catch (Exception ex)
            {
              //  return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }


        //[HttpGet("{eventId:int}")]
        //public async Task<ActionResult<EventViewModel>> Get(int eventId)
        //{
        //    try
        //    {
        //        var eventDTO = await _eventService.GetSingleEventAsync(eventId);
        //        var eventViewModel = _mapper.Map<EventViewModel>(eventDTO.Value);

        //        return Ok(eventViewModel);

        //    }
        //    catch (Exception)
        //    {

        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult<CreateEventViewModel>> Post(CreateEventViewModel createEventViewModel)
        //{
        //    try
        //    {
        //        var createEventDTO = _mapper.Map<CreateEventDTO>(createEventViewModel);
        //        var newEventDTO = await _eventService.CreateEventAsync(createEventDTO);

        //        if (newEventDTO != null)
        //        {
        //            var newCreateEventViewModel = _mapper.Map<CreateEventViewModel>(newEventDTO.Value);

        //            var url = _linkGenerator.GetPathByAction(HttpContext,
        //                "Get",
        //                values: new { eventId = newCreateEventViewModel.EventId });

        //            return Created(url, newCreateEventViewModel);


        //        }

        //        else
        //        {
        //            return BadRequest("Failed to save new Customer");
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }

        //}

        //[HttpPut("{eventId:int}")]
        //public async Task<ActionResult<EventViewModel>> Put([FromBody] EventViewModel eventViewModel, [FromRoute] int eventId)
        //{
        //    try
        //    {
        //        //await _customerService.UpdateCustomerAsync(customerId, customer);
        //        //return Ok();
        //        var eventDTO = _mapper.Map<EventDTO>(eventViewModel);

        //        await _eventService.UpdateCustomerAsync(eventId, eventDTO);

        //        var updatedEventDTO = await _eventService.GetSingleEventAsync(eventId);
        //        var updatedEventViewModel = _mapper.Map<EventViewModel>(updatedEventDTO.Value);

        //        return updatedEventViewModel;

        //    }
        //    catch (Exception)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        //    }

        //}




    //    [HttpDelete("{eventId:int}")]
    //    public async Task<IActionResult> Delete(int eventId)
    //    {
    //        try
    //        {
    //            var deleteEvent = await _eventService.DeleteEventAsync(eventId);
    //            return Ok();

    //        }
    //        catch (Exception)
    //        {
    //            return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
    //        }

    //        return BadRequest();
    //    }
    }
}

