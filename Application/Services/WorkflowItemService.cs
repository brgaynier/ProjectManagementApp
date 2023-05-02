using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.DTOs;
using AutoMapper.QueryableExtensions;
using Core;

namespace Application.Services
{
    public class WorkflowItemService : IWorkflowItemService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<WorkflowItemService> _logger;
        private readonly IMapper _mapper;
        private readonly IWorkflowService _workflowService;

        public WorkflowItemService(ApplicationDbContext dbContext, ILogger<WorkflowItemService> logger, IMapper mapper, IWorkflowService workflowService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _workflowService = workflowService ?? throw new ArgumentNullException(nameof(workflowService));
        }

        public async Task<IEnumerable<WorkflowItemDTO>> GetAllWorkflowItemsByWorkFlowIdAsync(int workFlowId, bool includeMember = false)
        {
            _logger.LogInformation($"Getting all Workflow Items");

            IQueryable<WorkFlowItem> query = _dbContext.WorkFlowItems;


            if (includeMember)
            {
                query = query
                .Include(t => t.Member);
            }

            // Add Query
            query = query
                .Where(t => t.WorkFlow.WorkFlowId == workFlowId);


            // Order It
            query = query.OrderBy(c => c.StartDate);

            var workflowItemDTOs = await query.ProjectTo<WorkflowItemDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return workflowItemDTOs;

        }

        public async Task<Response<WorkflowItemDTO?>> GetSingleWorkflowItemByWorkFlowIdAsync(int workFlowItemId, int workFlowId, bool includeMember = false)
        {
            _logger.LogInformation($"Getting a Work Flow Item {workFlowItemId}");

            IQueryable<WorkFlowItem> query = _dbContext.WorkFlowItems;

            if (query == null)
            {
                return new Response<WorkflowItemDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Workflow item does not exist" } } };

            }

            query = query
               .Where(t => t.WorkFlowItemId == workFlowItemId && t.WorkFlow.WorkFlowId == workFlowId);

            var workflowItemDTO = await query.ProjectTo<WorkflowItemDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<WorkflowItemDTO?> { Value = workflowItemDTO, Status = ResponseStatus.Ok };
        }

        public async Task<Response<CreateWorkflowItemDTO?>> CreateWorkflowItemAsync(int workflowId, int customerId, CreateWorkflowItemDTO createWorkflowItemDTO)
        {   
            var workflowDTO = await _workflowService.GetSingleWorkflowByCustomerIdAsync(customerId, workflowId);
            var workflow = _mapper.Map<WorkFlow>(workflowDTO);

            if (workflow == null)
            {
                return new Response<CreateWorkflowItemDTO?> { Value = null, Status = ResponseStatus.NotFound, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "No workflow found" } } };
            }

            var newWorkflowItem = _mapper.Map<WorkFlowItem>(createWorkflowItemDTO);
            newWorkflowItem.WorkflowId = workflowId;

            await _dbContext.WorkFlowItems.AddAsync(newWorkflowItem);
            await _dbContext.SaveChangesAsync();

            var workflowItemDTO = _mapper.Map<CreateWorkflowItemDTO>(newWorkflowItem);
            return new Response<CreateWorkflowItemDTO?> { Value = workflowItemDTO, Status = ResponseStatus.Ok };

        }
        public async Task UpdateWorkflowItemAsync(int workflowItemId, WorkflowItemDTO workflowItemDTO)
        {
            _logger.LogInformation($"Updating WorkFLowItem {workflowItemId}");

            var updateWorkFlowItem = await _dbContext.WorkFlowItems.FindAsync(workflowItemId);
            if (updateWorkFlowItem != null)
            {
                updateWorkFlowItem.Name = workflowItemDTO.Name;
                updateWorkFlowItem.Duration = workflowItemDTO.Duration;
                updateWorkFlowItem.StartDate = workflowItemDTO.StartDate;
                updateWorkFlowItem.DueDate = workflowItemDTO.DueDate;
                //     updateWorkFlowItem.IsCompleted = workFlowItem.IsCompleted;

                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<WorkflowItemDTO?> DeleteWorkflowItemAsync(int workflowId, int workflowItemId)
        {
            var workflowItem = await _dbContext.WorkFlowItems.FindAsync(workflowItemId);

            if (workflowItem != null)
            {
                _dbContext.WorkFlowItems.Remove(workflowItem);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedWorkflowItemDTO = _mapper.Map<WorkflowItemDTO>(workflowItem);
            return deletedWorkflowItemDTO;

        }
    }

}
