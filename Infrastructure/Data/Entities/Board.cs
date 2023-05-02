using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Infrastructure.Data.Entities
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }
        [Required]
        [DisplayName("Board Name")]
        public string? Title { get; set; }
        public IEnumerable<Block> Blocks { get; set; } = new List<Block>();
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted => DeletedAt != null;


        public int CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }

        // public IEnumerable<Customer> Customer { get; set; }

    }
}
