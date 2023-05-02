using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateWorkflowDTO
    {
        [Key]
        public int WorkFlowId { get; set; }
        [Required]
        public string? WorkFlowName { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }

        public IEnumerable<WorkflowItemDTO>? WorkFlowItems { get; set; }
       // public IEnumerable<ChecklistItem>? ChecklistItems { get; set; }

    }
}
