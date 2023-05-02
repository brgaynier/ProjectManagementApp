using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using ProjectManagementApp.Api.Data.Entities;
//using ProjectManagementApp.Api.Services;
//using ProjectManagementApp.Api.Services.Interfaces;
using Infrastructure.Data.Entities;
using Application.Services;
using Application.Services.Interfaces;
using ProjectManagementApp.Api.ViewModels;
using AutoMapper;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/customers/{customerId:int}/boards/{boardId:int}/blocks/{blockId:int}/cards/{cardId:int}/[controller]")]
    [ApiController]
    public class ChecklistsController : ControllerBase
    {
        private readonly IChecklistService _checklistService;
        private readonly ICardService _cardService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public ChecklistsController(IChecklistService checklistService, ICardService cardService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _checklistService = checklistService;
            _cardService = cardService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Checklist>> Get(int customerId, int boardId, int blockId, int cardId, bool includeMember = false, bool includeItem = true)
        {
            try
            {
                var results = await _checklistService.GetAllChecklistByCardIdAsync(cardId, includeItem);
                if (results == null) return NotFound();

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{checklistId:int}")]
        public async Task<ActionResult<Checklist>> Get(int customerId, int boardId, int blockId, int cardId, int checklistId, bool includeMember = false, bool includeItem = true)
        {
            try
            {
                var item = await _checklistService.GetSingleChecklistByCardIdAsync(checklistId, cardId, includeItem);
                if (item == null) return NotFound();

                return Ok(item);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Checklist>> Post(int customerId, int boardId, int blockId, int cardId, Checklist list)
        {       //TODO
            try
            {
         
            //    var newChecklist = await _checklistService.CreateCardAsync(boardId, cardId, card);
                _checklistService.Add(list);

                if (await _checklistService.SaveChangesAsync())
                {

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId, boardId, blockId, cardId, id = list.ChecklistId });

                    return Created(url, list);
                }
                else
                {
                    return BadRequest("Failed to save new checklist");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{checklistId:int}")]
        public async Task<ActionResult<Checklist>> Put([FromBody] Checklist list, [FromRoute] int checklistId)
        {
            try
            {
                await _checklistService.UpdateChecklistAsync(checklistId, list);
                return Ok();


            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{checklistId:int}")]
        public async Task<IActionResult> Delete(int customerId, int boardId, int blockId, int checklistId, int cardId)
        {
            try
            {
                var oldList = await _checklistService.GetSingleChecklistByCardIdAsync(checklistId, cardId);
                if (oldList == null) return NotFound($"List could not be found with Id of {checklistId}");

                _checklistService.Delete(oldList);

                if (await _checklistService.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }
    }
}
