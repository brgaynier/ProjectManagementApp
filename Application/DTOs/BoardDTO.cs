using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BoardDTO
    {

        public int BoardId { get; set; }
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<BlockDTO> Blocks { get; set; } = new List<BlockDTO>();


    }
}
