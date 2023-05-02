using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EventDTO
    {
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
        public DateTime? CreatedDate { get; set; }
    }
}
