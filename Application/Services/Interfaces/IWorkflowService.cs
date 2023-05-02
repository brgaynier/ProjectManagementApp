using Application.DTOs;
using Core;
using Infrastructure.Data.Entities;


namespace Application.Services.Interfaces
{
    public interface IWorkflowService
    {
        Task<IEnumerable<WorkflowDTO>> GetAllWorkflowsByCustomerIdAsync(int customerId, bool includeItems = true);
        Task<Response<WorkflowDTO?>> GetSingleWorkflowByCustomerIdAsync(int customerId, int workFlowId, bool includeItems = true, bool includeMember = false);     
        Task<Response<CreateWorkflowDTO?>> CreateWorkflowAsync(int customerId, CreateWorkflowDTO createWorkflowDTO);
        Task UpdateWorkflowAsync(int workFlowId, WorkflowDTO workflowDTO);
        Task<WorkflowDTO?> DeleteWorkflowAsync(int customerId, int workflowId);
    }
}