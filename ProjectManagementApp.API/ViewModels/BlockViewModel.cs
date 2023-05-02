//using ProjectManagementApp.Api.Data.Entities;
using Application.DTOs;
using Infrastructure.Data.Entities;

namespace ProjectManagementApp.Api.ViewModels
{
    public class BlockViewModel
    {
        public int BlockId { get; set; }
        public string? BlockName { get; set; }
       // public ICollection<CardViewModel> Cards { get; set; } = new List<CardViewModel>();
        public ICollection<CardViewModel> Cards { get; set; } = new List<CardViewModel>();
        public int BoardId { get; set; }


        //public Board? Board { get; set; }
        //public int BoardId { get; set; }

    }
}
