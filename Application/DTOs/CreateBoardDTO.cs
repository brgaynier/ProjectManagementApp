﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateBoardDTO
    {
        public int BoardId { get; set; }
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<BlockDTO> Blocks { get; set; } = new List<BlockDTO>();
    }
}
