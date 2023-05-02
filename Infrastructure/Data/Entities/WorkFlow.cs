using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization.Metadata;

namespace Infrastructure.Data.Entities
{
    public class WorkFlow
    {
        [Key]
        public int WorkFlowId { get; set; }
        [Required]
        public string? WorkFlowName { get; set; }
        public Customer? Customer { get; set; }
        public IEnumerable<WorkFlowItem>? WorkFlowItems { get; set; }
        public IEnumerable<ChecklistItem>? ChecklistItems { get; set; }
        public int CustomerId { get; set; }

        // bool hasBlocker **

    }
}
