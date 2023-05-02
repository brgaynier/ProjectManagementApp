using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels
{
    public class WorkflowItemViewModel : ItemViewModel
    {
        public int WorkflowItemId { get; set; }
     //   public WorkFlow? Workflow { get; set; }
        public int WorkflowId { get; set; }

    }
}
