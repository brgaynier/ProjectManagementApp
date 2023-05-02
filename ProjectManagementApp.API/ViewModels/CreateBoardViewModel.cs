namespace ProjectManagementApp.Api.ViewModels
{
    public class CreateBoardViewModel
    {
        public int BoardId { get; set; }
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<BlockViewModel> Blocks { get; set; } = new List<BlockViewModel>();
    }
}
