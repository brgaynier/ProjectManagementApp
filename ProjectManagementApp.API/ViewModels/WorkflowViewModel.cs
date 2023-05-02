using Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Api.ViewModels
{
    public class WorkflowViewModel
    {
        [Key]
        public int WorkFlowId { get; set; }
        [Required]
        public string? WorkFlowName { get; set; }
   //     public Customer? Customer { get; set; }
        public int CustomerId { get; set; }

        public IEnumerable<WorkflowItemViewModel>? WorkFlowItems { get; set; }
      //  public IEnumerable<ChecklistItem>? ChecklistItems { get; set; }
    }
}
