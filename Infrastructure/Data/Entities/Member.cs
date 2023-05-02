using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Bio { get; set; }

        //  public ChecklistItem? ChecklistItem { get; set; }
        public Card? Card { get; set; }
        //  public IEnumerable<Card>? Cards { get; set; }    
    }
}