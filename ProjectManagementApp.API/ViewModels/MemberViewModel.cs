//using ProjectManagementApp.Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels
{
    public class MemberViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }

        //  public ChecklistItem? ChecklistItem { get; set; }
        public Card? Card { get; set; }
        //  public IEnumerable<Card>? Cards { get; set; }  
    }
}
