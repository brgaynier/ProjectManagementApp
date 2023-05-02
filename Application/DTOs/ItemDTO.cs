using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ItemDTO
    {
        [Required]
        public string? Name { get; set; }
        [DisplayName("Estimated hours of work")]
        public int Duration { get; set; }
      //  public IEnumerable<Member>? Member { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
