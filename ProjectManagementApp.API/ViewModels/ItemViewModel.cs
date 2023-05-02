using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjectManagementApp.Api.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        public string? Name { get; set; }
        [DisplayName("Estimated hours of work")]
        public int Duration { get; set; }
        //  public IEnumerable<Member>? Member { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
