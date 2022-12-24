using Microsoft.EntityFrameworkCore;
using Npgsql;

using BloodBankLibrary.Core.Centers;
using BloodBankLibrary.Core.Users;
using BloodBankLibrary.Core.Forms;
using BloodBankLibrary.Core.Appointments;
using BloodBankLibrary.Core.Admins;
using BloodBankLibrary.Core.Staffs;
using BloodBankLibrary.Core.Donors;

using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Core.Materials;

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
            modelBuilder.Entity<Staff>().Property(s => s.Address).HasColumnType("jsonb");
            modelBuilder.Entity<BloodCenter>().Property(b => b.Address).HasColumnType("jsonb");

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

            BloodCenter bc1 = new BloodCenter(1,"Center 1", "Futoska 62,Novi Sad,Srbija", "Blood transfusion center.", 4.9, "12:00", "18:00" );
            BloodCenter bc2 = new BloodCenter (2, "Center 2", "Bulevar Oslobodjenja 111,Novi Sad,Srbija", "Blood transfusion center.", 3.7, "08:00", "14:00" );
            BloodCenter bc3 = new BloodCenter (3,"Center 3", "Strazilovska 18,Novi Sad,Srbija", "Blood transfusion center.", 5.0, "09:00", "16:00" );
            BloodCenter bc4 = new BloodCenter (4,"Center 4", "Vere Petrovic 1,Novi Sad,Srbija", "Blood transfusion center.", 4.2, "13:00", "17:00" );
            modelBuilder.Entity<BloodCenter>().HasData(bc1,bc2,bc3,bc4);

            Donor d = new Donor() {Id=1, Name = "Emilija", Surname = "Medic", Email = "donor", Jmbg = 34242423565, Address = new Address("Bore Prodanovica 11,Novi Sad,Srbija"), Gender = Gender.FEMALE, PhoneNumber = 381629448332, Profession = "student", Workplace = "Fakultet Tehnickih Nauka", Strikes = 0 };
            modelBuilder.Entity<Donor>().HasData(d);


            Admin a=new Admin () {Id=1, Email="admin",Name="Marko", Surname= "Dobrosavljevic" };
            modelBuilder.Entity<Admin>().HasData(a);

            Staff s = new Staff { Id = 1, Email = "staff", Name = "Milan", Surname = "Miric", CenterId = 1 };
            modelBuilder.Entity<Staff>().HasData(s);

            User u1 = new User { Id = 1, IdByType =1,Name = "Marko", Surname = "Dobrosavljevic", Email = "admin", Active=true, Token = null, Password = "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==",  UserType= UserType.ADMIN };
            User u2 = new User { Id = 2, IdByType = 1, Name = "Emilija", Surname = "Medic", Email="donor", Active = true, Token = null, Password = "APuucZwPYpx2awM5SRWZ55yMOqwvnKdxTyFmtxSskpMzABHMEILvphRla+B4hvTmhw==", UserType = UserType.DONOR };
            User u3 = new User { Id = 3, IdByType = 1, Email = "staff", Name = "Milan", Surname = "Miric", Active=true, Token=null,  Password= "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g==", UserType = UserType.STAFF };
           
            modelBuilder.Entity<User>().HasData(u1, u2, u3);

            base.OnModelCreating(modelBuilder);
        }
    }
}
