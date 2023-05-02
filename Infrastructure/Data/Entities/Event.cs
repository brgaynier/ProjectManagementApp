using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class Event
    {
   //     public int Id { get; set; }
   //     public DateTime Start { get; set; }
   //     public DateTime End { get; set; }
   //     public int ResourceId { get; set; }
   //     public string? Description { get; set; }

        public int EventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Start { get; set; }
        //public DateTime? EndDate { get; set; }
        public string? End { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool? IsFullDayEvent { get; set; }
        public bool? IsRecurring { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    //public class Resource
    //{
    //    public int Id { get; set; }
    //    public string? Title { get; set; }

    //}
  
}
