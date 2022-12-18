using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;

namespace BloodBankLibrary.Settings
{
    public class BloodBankDbContext : DbContext
    {
        public DbSet<BloodCenter> BloodCenters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Donor> Donors { get; set; }

        public BloodBankDbContext(DbContextOptions<BloodBankDbContext> options) : base(options) {

            NpgsqlConnection.GlobalTypeMapper.MapEnum<Gender>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UserType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AppointmentStatus>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Gender>();
            modelBuilder.HasPostgresEnum<UserType>();
            modelBuilder.HasPostgresEnum<AppointmentStatus>();

            //ovde punimo i tabele!
            base.OnModelCreating(modelBuilder);
        }
    }
}
