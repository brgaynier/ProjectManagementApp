using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BlockDTO
    {
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
    //    public ICollection<CardDTO> Cards { get; set; } = new List<CardDTO>();
        public ICollection<CardDTO> Cards { get; set; } = new List<CardDTO>();

        public Board? Board { get; set; }
        public int BoardId { get; set; }
    }
}
