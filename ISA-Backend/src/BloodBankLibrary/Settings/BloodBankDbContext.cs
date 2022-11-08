using BloodBankLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace BloodBankLibrary.Settings
{
    public class BloodBankDbContext : DbContext
    {
        public DbSet<BloodCenter> BloodCenters { get; set; }
        public DbSet<User> Users { get; set; }
        public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
