using AutoMapper.Execution;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class Checklist
    {
        [Key]
        public int ChecklistId { get; set; }
        [Required]
        public string? Title { get; set; }
        public IEnumerable<ChecklistItem>? ChecklistItems { get; set; }
        public Member? Member { get; set; }
        public DateTime DateTime { get; set; }
        public Card? Card { get; set; }
        public bool HasBlocker { get; set; }


    }
}