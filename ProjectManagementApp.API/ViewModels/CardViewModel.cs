//using ProjectManagementApp.Api.Data.Entities;
using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels
{
    public class CardViewModel
    {
        public int CardId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public string? DueDate { get; set; }   //Due Date/Time,  Due Date Reminder,  (have card alert and/or turn red at the reminder time)

        //public attachement?  how to attach things to a model?
        //public Cover? Cover { get; set; } //this will be a color for the header on the card.
        //public IEnumerable<Member>? Member { get; set; }
        //public IEnumerable<Checklist>? Checklist { get; set; }
        public string? Label { get; set; }
        //public Block? Block { get; set; }
        public int BlockId { get; set; }


    }
}
