using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class Block
    {
        [Key]
        public int BlockId { get; set; }
        [Required]
        public string? BlockName { get; set; }
        public IEnumerable<Card> Cards { get; set; } = new List<Card>();

        public Board? Board { get; set; }
        public int BoardId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted => DeletedAt != null;
        public bool HasBlocker { get; set; }

    }
}
