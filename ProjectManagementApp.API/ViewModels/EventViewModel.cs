namespace ProjectManagementApp.Api.ViewModels
{
    public class EventViewModel
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
