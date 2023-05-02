using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Infrastructure.Data.Entities
{
    public class Item
    {
        [Required]
        public string? Name { get; set; }
        [DisplayName("Estimated hours of work")]
        public int Duration { get; set; }
        public IEnumerable<Member>? Member { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted => DeletedAt != null;
        public bool HasBlocker { get; set; }
        //     public bool IsCompleted { get; set; } = false;


    }
}
