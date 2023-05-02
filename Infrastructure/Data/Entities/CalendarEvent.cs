using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class CalendarEvent
    {
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }   
        public string? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public string? EndDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool? IsFullDayEvent { get; set; }
        public bool? IsRecurring { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
