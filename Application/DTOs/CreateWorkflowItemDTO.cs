using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateWorkflowItemDTO : CreateItemDTO
    {
        public int WorkFlowItemId { get; set; }
        public WorkFlow? Workflow { get; set; }
        public int WorkflowId { get; set; }

    }
}
