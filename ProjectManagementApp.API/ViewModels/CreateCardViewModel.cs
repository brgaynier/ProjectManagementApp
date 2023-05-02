namespace ProjectManagementApp.Api.ViewModels
{
    public class CreateCardViewModel
    {
        public int CardId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;
        public string? Label { get; set; } = string.Empty;
        public int BlockId { get; set; }
    }
}
