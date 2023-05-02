using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data.Entities;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using ProjectManagementApp.Api.ViewModels;
using Application.DTOs;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/customers/{customerId:int}/[controller]")]     
    [ApiController]
    public class WorkflowsController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;
        private readonly ICustomerService _customerService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public WorkflowsController(IWorkflowService workflowService, ICustomerService customerService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _workflowService = workflowService;
            _customerService = customerService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<WorkflowViewModel>> Get(int customerId, bool includeItems = true)
        {
            try
            {
                var workflowsDTO = await _workflowService.GetAllWorkflowsByCustomerIdAsync(customerId, includeItems);
                var workflowsViewModel = _mapper.Map<WorkflowViewModel[]>(workflowsDTO);

                return Ok(workflowsViewModel);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpGet("{workFlowId:int}")]
        public async Task<ActionResult<WorkflowViewModel>> Get(int customerId, int workFlowId, bool includeItems = true)
        {
            try
            {
                var workflowDTO = await _workflowService.GetSingleWorkflowByCustomerIdAsync(customerId, workFlowId, includeItems);
                var workflowViewModel = _mapper.Map<WorkflowViewModel>(workflowDTO.Value);

                return Ok(workflowViewModel);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateWorkflowViewModel>> Post(int customerId, CreateWorkflowViewModel createWorkflowViewModel)
        {
            try
            {
                var createWorkflowDTO = _mapper.Map<CreateWorkflowDTO>(createWorkflowViewModel);

                var newWorkflowDTO = await _workflowService.CreateWorkflowAsync(customerId, createWorkflowDTO);

                if (newWorkflowDTO != null)
                {
                    var newCreateWorkflowViewModel = _mapper.Map<CreateWorkflowViewModel>(newWorkflowDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId, id = newCreateWorkflowViewModel.WorkFlowId });

                    return Created(url, newCreateWorkflowViewModel);
                }
                else
                {
                    return BadRequest("Failed to save new Workflow");
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{workFlowId:int}")]
        public async Task<ActionResult<WorkflowViewModel>> Put([FromBody] WorkflowViewModel workflowViewModel, [FromRoute] int workFlowId, int customerId)
        {
            try
            {
                var workflowDTO = _mapper.Map<WorkflowDTO>(workflowViewModel);

                await _workflowService.UpdateWorkflowAsync(workFlowId, workflowDTO);

                var updatedWorkflowDTO = await _workflowService.GetSingleWorkflowByCustomerIdAsync(customerId, workFlowId);
                var updatedWorkflowViewModel = _mapper.Map<WorkflowViewModel>(updatedWorkflowDTO.Value);

                return updatedWorkflowViewModel;

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpDelete("{workFlowId:int}")]
        public async Task<IActionResult> Delete(int customerId, int workFlowId)
        {
            try
            {
                var deleteWorkflow = await _workflowService.DeleteWorkflowAsync(customerId, workFlowId);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
    }
}
