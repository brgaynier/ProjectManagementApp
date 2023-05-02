using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Phone]
        public string? CustomerPhone { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? CityTown { get; set; }
        public string? StateProvince { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }

        public IEnumerable<Board> Boards { get; set; } = new List<Board>();
        public IEnumerable<WorkFlow>? WorkFlows { get; set; }
        public IEnumerable<ChangeOrder>? ChangeOrders { get; set; }

        //public int BoardId {get; set;}

        //public Board? Board { get; set; }



    }
}
