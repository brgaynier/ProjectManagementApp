//using Microsoft.Data.SqlClient.DataClassification;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Infrastructure.Data.Entities
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public IEnumerable<Member>? Member { get; set; }
        //   public Member? Member { get; set; }
        public IEnumerable<Checklist>? Checklist { get; set; }
        public string? Label { get; set; }
        public string? DueDate { get; set; } //Due Date/Time,  Due Date Reminder,  (have card alert and/or turn red at the reminder time)
        //public attachement?  how to attach things to a model?
        public Cover? Cover { get; set; } //this will be a color for the header on the card.
        public Block? Block { get; set; }

        public int BlockId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? AssignedAt { get; set; }
        public bool IsAssigned => AssignedAt != null;
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted => CompletedAt != null;
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted => DeletedAt != null;
        public bool HasBlocker { get; set; }
        //    public bool HasMember { get; set; } = false;




    }
}
