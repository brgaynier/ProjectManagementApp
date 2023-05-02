using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels
{
    public class ChangeOrderViewModel
    {
        public int ChangeOrderId { get; set; }
        public int ChangeOrderNumber { get; set; }
        public string? ChangeOrderName { get; set; }
        public string? Description { get; set; }
        public int AmountOwed { get; set; }
  //      public Customer? Customer { get; set; }
        public int CustomerId { get; set; }

    }
}

