namespace ProjectManagementApp.Api.ViewModels
{
    public class CreateBlockViewModel
    {
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
        public ICollection<CardViewModel> Cards { get; set; } = new List<CardViewModel>();
        public int BoardId { get; set; }

    }
}
