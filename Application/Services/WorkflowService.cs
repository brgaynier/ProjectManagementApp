using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
//using ProjectManagementApp.Api.Data;
//using ProjectManagementApp.Api.Data.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.DTOs;
using Core;
using AutoMapper.QueryableExtensions;

namespace Application.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<WorkflowService> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public WorkflowService(ApplicationDbContext dbContext, ILogger<WorkflowService> logger, IMapper mapper, ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    
        public async Task<IEnumerable<WorkflowDTO>> GetAllWorkflowsByCustomerIdAsync(int customerId, bool includeItems = true)
        {
            _logger.LogInformation($"Getting all Work Flows for customer {customerId}");

            IQueryable<WorkFlow> query = _dbContext.WorkFlows;
            //.Include(c => c.Blocks)
            //.ThenInclude(t => t.Cards);

            if (includeItems)
            {
                query = query
                    .Include(c => c.WorkFlowItems)
                    .Include(c => c.ChecklistItems);
            }

            // Order It
            //  query = query.OrderBy(c => c.DueDate);

            var workflowDTOs = await query.ProjectTo<WorkflowDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return workflowDTOs;
        }
        public async Task<Response<WorkflowDTO?>> GetSingleWorkflowByCustomerIdAsync(int customerId, int workFlowId, bool includeItems = true, bool includeMember = false)
        {
            _logger.LogInformation($"Getting single Work Flow for customer {workFlowId}");

            IQueryable<WorkFlow> query = _dbContext.WorkFlows;

            if (query == null)
            {
                return new Response<WorkflowDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Workflow does not exist" } } };

            }

            if (includeItems)
            {
                query = query
                    .Include(c => c.WorkFlowItems)
                    .Include(c => c.ChecklistItems);
            }

            query = query
               .Where(t => t.WorkFlowId == workFlowId && t.Customer.CustomerId == customerId);

            // Query It
            //query = query.Where(c => c.WorkFlowItemId == workFlowItemId);

            var workflowDTO = await query.ProjectTo<WorkflowDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<WorkflowDTO?> { Value = workflowDTO, Status = ResponseStatus.Ok };

        }

        public async Task<Response<CreateWorkflowDTO?>> CreateWorkflowAsync(int customerId, CreateWorkflowDTO createWorkflowDTO)
        {   //use DTO to access database.. front end doesn't need to know what the DB looks like
            //validations first, then actions, then return

            var customerDTO = await _customerService.GetSingleCustomerAsync(customerId);
            var customer = _mapper.Map<Customer>(customerDTO);

            if (customer == null)
            {
                return new Response<CreateWorkflowDTO?> { Value = null, Status = ResponseStatus.NotFound, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "No Customer found" } } };
            }

            var newWorkflow = _mapper.Map<WorkFlow>(createWorkflowDTO);
            newWorkflow.CustomerId = customerId;

            await _dbContext.WorkFlows.AddAsync(newWorkflow);
            await _dbContext.SaveChangesAsync();

            var workflowDTO = _mapper.Map<CreateWorkflowDTO>(newWorkflow);
            return new Response<CreateWorkflowDTO?> { Value = workflowDTO, Status = ResponseStatus.Ok };

        }

        public async Task UpdateWorkflowAsync(int workFlowId, WorkflowDTO workflowDTO)
        {
            _logger.LogInformation($"Updating WorkFLow {workFlowId}");

            var updateWorkflow = await _dbContext.WorkFlows.FindAsync(workFlowId);
            if (updateWorkflow != null)
            {
                updateWorkflow.WorkFlowName = workflowDTO.WorkFlowName;

                _mapper.Map(updateWorkflow, workflowDTO);

                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<WorkflowDTO?> DeleteWorkflowAsync(int customerId, int workflowId)
        {
            var workflow = await _dbContext.WorkFlows.FindAsync(workflowId);

            if (workflow != null)
            {
                _dbContext.WorkFlows.Remove(workflow);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedWorkflowDTO = _mapper.Map<WorkflowDTO>(workflow);
            return deletedWorkflowDTO;

        }


    }
}
