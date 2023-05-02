namespace ProjectManagementApp.Api.ViewModels
{
    public class CreateCustomerViewModel
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? CityTown { get; set; }
        public string? StateProvince { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public ICollection<BoardViewModel> Boards { get; set; } = new List<BoardViewModel>();


    }
}
