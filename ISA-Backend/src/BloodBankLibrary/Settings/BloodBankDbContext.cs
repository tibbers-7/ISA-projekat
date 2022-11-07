using BloodBankLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace BloodBankLibrary.Settings
{
    public class BloodBankDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", Floor = 1 },
                new Room() { Id = 2, Number = "204", Floor = 2 },
                new Room() { Id = 3, Number = "305B", Floor = 3 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
