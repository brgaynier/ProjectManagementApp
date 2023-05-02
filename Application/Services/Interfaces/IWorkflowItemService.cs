using Application.DTOs;
using Core;
using Infrastructure.Data.Entities;


namespace Application.Services.Interfaces
{
    public interface IWorkflowItemService
    {
        Task<IEnumerable<WorkflowItemDTO>> GetAllWorkflowItemsByWorkFlowIdAsync(int workFlowId, bool includeMember = false);
        Task<Response<WorkflowItemDTO?>> GetSingleWorkflowItemByWorkFlowIdAsync(int workFlowItemId, int workFlowId, bool includeMember = false);
        Task<Response<CreateWorkflowItemDTO?>> CreateWorkflowItemAsync(int workflowId, int customerId, CreateWorkflowItemDTO createWorkflowItemDTO);
        Task UpdateWorkflowItemAsync(int workflowItemId, WorkflowItemDTO workflowItemDTO);
        Task<WorkflowItemDTO?> DeleteWorkflowItemAsync(int workflowId, int workflowItemId);
    }
}