using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using ProjectManagementApp.Api.Data.Entities;
//using ProjectManagementApp.Api.Services.Interfaces;
using Infrastructure.Data.Entities;
using Application.Services;
using Application.Services.Interfaces;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/customers/{customerId:int}/boards/{boardId:int}/blocks/{blockId:int}/cards/{cardId:int}/checklists/{checklistId:int}/[controller]")]
    [ApiController]
    public class ChecklistItemsController : ControllerBase
    {
        private readonly IChecklistService _checklistService;
        private readonly LinkGenerator _linkGenerator;

        public ChecklistItemsController(IChecklistService checklistService, LinkGenerator linkGenerator)
        {
            _checklistService = checklistService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<ChecklistItem>> Get(int customerId, int boardId, int blockId, int cardId, int checklistId, bool includeMember = false)
        {
            try
            {
                var results = await _checklistService.GetAllChecklistItemsByChecklistIdAsync(checklistId);
                if (results == null) return NotFound();

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{checklistItemId:int}")]
        public async Task<ActionResult<ChecklistItem>> Get(int customerId, int boardId, int blockId, int cardId, int checklistId, int checklistItemId, bool includeMember = false)
        {
            try
            {
                var item = await _checklistService.GetSingleChecklistItemByChecklistIdAsync(checklistItemId, checklistId);
                if (item == null) return NotFound();

                return Ok(item);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ChecklistItem>> Post(int customerId, int boardId, int blockId, int cardId, int checklistId, ChecklistItem item)
        {
            try
            {
                var checklist = await _checklistService.GetSingleChecklistByCardIdAsync(checklistId, cardId);
                if (checklist == null) return BadRequest("Card does not exist.");

                item.Checklist= checklist;

                _checklistService.Add(item);

                if (await _checklistService.SaveChangesAsync())
                {

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId, boardId, blockId, cardId, checklistId, id = item.ChecklistItemId });

                    return Created(url, item);
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

        [HttpPut("{checklistItemId:int}")]
        public async Task<ActionResult<ChecklistItem>> Put([FromBody] ChecklistItem item, [FromRoute] int checklistItemId)
        {
            try
            {
                await _checklistService.UpdateChecklistItemAsync(checklistItemId, item);
                return Ok();


            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{checklistItemId:int}")]
        public async Task<IActionResult> Delete(int customerId, int boardId, int blockId, int checklistId, int checklistItemId, int cardId)
        {
            try
            {
                var oldItem = await _checklistService.GetSingleChecklistItemByChecklistIdAsync(checklistItemId, checklistId);
                if (oldItem == null) return NotFound($"Item could not be found with Id of {checklistItemId}");

                _checklistService.Delete(oldItem);

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
