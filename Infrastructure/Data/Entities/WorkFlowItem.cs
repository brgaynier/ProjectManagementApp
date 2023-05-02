using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Infrastructure.Data.Entities
{
    public class WorkFlowItem : Item
    {
        [Key]
        public int WorkFlowItemId { get; set; }
        public WorkFlow? WorkFlow { get; set; }
        public int WorkflowId { get; set; }

        //[Required]
        //public string? Name { get; set; }
        //[DisplayName("Estimated hours of work")]
        //public int Duration { get; set; }
        //public IEnumerable<Member>? Member { get; set; }

        //public DateTime? CreatedAt { get; set; } = DateTime.Now;
        //public DateTime StartDate { get; set; }
        //public DateTime DueDate { get; set; }
        //public Card? Card { get; set; }

        //public DateTime? DeletedAt { get; set; }
        //public bool IsDeleted => DeletedAt != null;
    }
}
