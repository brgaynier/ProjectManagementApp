//using ProjectManagementApp.Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels
{
    public class BoardViewModel
    {
        public int BoardId { get; set; }
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<BlockViewModel> Blocks { get; set; } = new List<BlockViewModel>();

        //public IEnumerable<Block> Blocks { get; set; } = new List<Block>();
        //public Customer? Customer { get; set; }   //don't need - this links back to the original Customer and would be confusing

    }
}
