using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels
{
    public class CreateWorkflowItemViewModel : CreateItemViewModel
    {
        public int WorkFlowItemId { get; set; }
        public WorkFlow? Workflow { get; set; }
        public int WorkflowId { get; set; }

    }
}
