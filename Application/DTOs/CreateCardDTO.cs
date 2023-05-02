using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateCardDTO
    {
        public int CardId { get; set; }


        [Required] //can add validations here
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public DateTime DueDate { get; set; }
        public string? Label { get; set; }
     //   public Block? Block { get; set; }
       public int BlockId { get; set; }

    }
}
