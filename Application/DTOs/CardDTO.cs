using Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CardDTO
    {
         public int CardId { get; set; }

        [Required] //can add validations here
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public string? DueDate { get; set; }
        public string? Label { get; set; }
        public Block? Block { get; set; }
        public int BlockId { get; set; }


    }

}
