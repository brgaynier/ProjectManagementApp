using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class ChecklistItem : Item
    {
        [Key]
        public int ChecklistItemId { get; set; }
        public Checklist? Checklist { get; set; }
    }
}
