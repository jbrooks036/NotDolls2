using Microsoft.EntityFrameworkCore;


namespace NotDolls2.Models
{
    public class NotDolls2Context : DbContext
    {
        public NotDolls2Context(DbContextOptions<NotDolls2Context> options) : base(options)
        { }

        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryImage> Image { get; set; }
        public DbSet<Geek> Geek { get; set; }
    }
}
