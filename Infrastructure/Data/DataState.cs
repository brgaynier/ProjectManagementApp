using Infrastructure.Data.Entities;
using System.Net.Sockets;

namespace Infrastructure.Data
{
    public class DataState
    {
        public List<Board> Boards = new();
        public List<Block> Blocks = new();
        public List<Card> Cards = new();
    }
}
