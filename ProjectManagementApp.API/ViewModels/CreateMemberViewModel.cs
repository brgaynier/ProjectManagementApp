namespace ProjectManagementApp.Api.ViewModels
{
    public class CreateMemberViewModel
    {
        public int MemberId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
    }
}
