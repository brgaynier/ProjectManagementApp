using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Infrastructure.Data.Entities;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using ProjectManagementApp.Api.ViewModels;
using Application.DTOs;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/customers/{customerId:int}/workFlows/{workFlowId:int}/[controller]")]
    [ApiController]
    public class WorkflowItemsController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;
        private readonly IWorkflowItemService _workflowItemService;
        private readonly ICardService _cardService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public WorkflowItemsController(IWorkflowService workflowService, IWorkflowItemService workflowItemService, ICardService cardService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _workflowService = workflowService;
            _workflowItemService = workflowItemService;
            _cardService = cardService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<WorkflowItemViewModel>> Get(int workflowId, bool includeItems = false)
        {
            try
            {
                var workflowItemsDTO = await _workflowItemService.GetAllWorkflowItemsByWorkFlowIdAsync(workflowId, includeItems);
                var workflowItemsViewModel = _mapper.Map<WorkflowItemViewModel[]>(workflowItemsDTO);

                return Ok(workflowItemsViewModel);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{workFlowItemId:int}")]
        public async Task<ActionResult<WorkflowItemViewModel>> Get(int customerId, int workflowId, int workFlowItemId, bool includeItems = true)
        {
            try
            {
                var workflowItemDTO = await _workflowItemService.GetSingleWorkflowItemByWorkFlowIdAsync(workFlowItemId, workflowId, includeItems);
                var workflowItemViewModel = _mapper.Map<WorkflowItemViewModel>(workflowItemDTO.Value);

                return Ok(workflowItemViewModel);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateWorkflowItemViewModel>> Post(int customerId, int workflowId,  CreateWorkflowItemViewModel createWorkflowItemViewModel)
        {
            try
            {
                var createWorkflowItemDTO = _mapper.Map<CreateWorkflowItemDTO>(createWorkflowItemViewModel);

                var newWorkflowItemDTO = await _workflowItemService.CreateWorkflowItemAsync(workflowId, customerId, createWorkflowItemDTO);

                if (newWorkflowItemDTO != null)
                {
                    var newCreateWorkflowItemViewModel = _mapper.Map<CreateWorkflowItemViewModel>(newWorkflowItemDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new {customerId, workflowId, id = newCreateWorkflowItemViewModel.WorkFlowItemId });

                    return Created(url, newCreateWorkflowItemViewModel);
                }
                else
                {
                    return BadRequest("Failed to save new Workflow Item");
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{workFlowItemId:int}")]
        public async Task<ActionResult<WorkflowItemViewModel>> Put([FromBody] WorkflowItemViewModel workflowItemViewModel, [FromRoute] int workFlowItemId, int workflowId)
        { 
            try
            {
                var workflowItemDTO = _mapper.Map<WorkflowItemDTO>(workflowItemViewModel);

                await _workflowItemService.UpdateWorkflowItemAsync(workFlowItemId, workflowItemDTO);

                var updatedWorkflowItemDTO = await _workflowItemService.GetSingleWorkflowItemByWorkFlowIdAsync(workFlowItemId, workflowId);
                var updatedWorkflowItemViewModel = _mapper.Map<WorkflowItemViewModel>(updatedWorkflowItemDTO.Value);

                return updatedWorkflowItemViewModel;

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpDelete("{workFlowItemId:int}")]
        public async Task<IActionResult> Delete(int workflowId, int workFlowItemId)
        {
            try
            {
                var deleteWorkflowItem = await _workflowItemService.DeleteWorkflowItemAsync(workflowId, workFlowItemId);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
