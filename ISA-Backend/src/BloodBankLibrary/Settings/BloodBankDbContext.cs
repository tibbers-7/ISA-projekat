using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
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


            modelBuilder.Entity<Donor>().Property(d => d.Address).HasColumnType("jsonb");

            Question[] questions= new Question[] { new Question(1, "Have you donated blood in the last 6 months?"), 
                                                    new Question(2, "Have you ever been rejected as a blood donor?"),
                                                    new Question(3,"Do you currently feel healthy and rested enough to donate blood?"),
                                                    new Question(4,"Have you eaten anything prior to your arrival to donate blood?"),
                                                    new Question(5,"Did you drink any alcohol in the last 6 hours?"),
                                                    new Question(6,"Have you had any tattoos or piercings done in the last 6 months?"),
                                                    new Question(7,"Have you ever consumed any type of opioids?"),
                                                    new Question(8,"Have you ever had unsafe sexual intercourse with a person suffering from HIV?")
                                                  };
            modelBuilder.Entity<Question>().HasData(questions);

            base.OnModelCreating(modelBuilder);
        }
    }
}
