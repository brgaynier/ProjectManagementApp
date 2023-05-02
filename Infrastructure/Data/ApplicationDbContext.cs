using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Block> Blocks { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<ChangeOrder> ChangeOrders { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<WorkFlow> WorkFlows { get; set; }
        public DbSet<WorkFlowItem> WorkFlowItems { get; set; }



    }
}
