using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomerDTO
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
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }

        public ICollection<BoardDTO> Boards { get; set; } = new List<BoardDTO>();
        public ICollection<WorkflowDTO>? WorkFlows { get; set; } = new List<WorkflowDTO>();
        public ICollection<ChangeOrderDTO>? ChangeOrders { get; set; } = new List<ChangeOrderDTO>();
    }
}
