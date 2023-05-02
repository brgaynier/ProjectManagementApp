using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateChangeOrderDTO
    {
        public int ChangeOrderId { get; set; }
        public int ChangeOrderNumber { get; set; }
        public string? ChangeOrderName { get; set; }
        public string? Description { get; set; }
        public int AmountOwed { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }

    }
}

