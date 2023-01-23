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
using Microsoft.EntityFrameworkCore.Internal;
using BloodBankLibrary.Core.Addresses;
using Microsoft.AspNetCore.SignalR;

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
        public DbSet<CenterAddress> Addresses { get; set; }

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

             BloodCenter bc1 = new BloodCenter(1,"Center 1","Blood transfusion center.", 4.9, "12:00:00", "18:00:00" );
             BloodCenter bc2 = new BloodCenter (2, "Center 2", "Blood transfusion center.", 3.7, "08:00:00", "14:00:00" );
             BloodCenter bc3 = new BloodCenter (3,"Center 3", "Blood transfusion center.", 5.0, "09:00:00", "16:00:00" );
             BloodCenter bc4 = new BloodCenter (4,"Center 4", "Blood transfusion center.", 4.2, "13:00:00", "17:00:00" );
             modelBuilder.Entity<BloodCenter>().HasData(bc1,bc2,bc3,bc4);

            CenterAddress a1 = new CenterAddress { Id=1, City= "Novi Sad", StreetAddress= "Futoska 62", CenterId=1, Country = "Srbija" };
            CenterAddress a2 = new CenterAddress { Id = 2, City = "Novi Sad", StreetAddress = "Bulevar Oslobodjenja 111", CenterId = 2, Country = "Srbija" }; 
            CenterAddress a3 = new CenterAddress { Id = 3, City = "Novi Sad", StreetAddress = "Strazilovska 18", CenterId = 3, Country = "Srbija" }; 
            CenterAddress a4 = new CenterAddress { Id = 4, City = "Novi Sad", StreetAddress = "Vere Petrovic 1", CenterId = 4, Country = "Srbija" }; 
            modelBuilder.Entity<CenterAddress>().HasData(a1, a2, a3, a4);

            Donor d = new Donor() {Id=1, Name = "Emilija", Surname = "Medic", Email = "donor", Jmbg = 34242423565, AddressString = "Ise Bajica 1,Novi Sad,Srbija", Gender = Gender.FEMALE, PhoneNumber = 381629448332, Profession = "student", Workplace = "Fakultet Tehnickih Nauka", Strikes = 0 };
            modelBuilder.Entity<Donor>().HasData(d);


            Admin a=new Admin () {Id=1, Email="admin",Name="Marko", Surname= "Dobrosavljevic" };
            modelBuilder.Entity<Admin>().HasData(a);

            Staff s = new Staff { Id = 1, Email = "staff", Name = "Milan", Surname = "Miric", AddressString = "Bore Prodanovica 22,Novi Sad,Srbija",Gender=Gender.MALE,Jmbg=47387297437,PhoneNumber=3816298437, CenterId = 1 };
            modelBuilder.Entity<Staff>().HasData(s);

            User u1 = new User { Id = 1, IdByType =1,Name = "Marko", Surname = "Dobrosavljevic", Email = "admin", Active=true, Token = null, Password = "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==",  UserType= UserType.ADMIN };
            User u2 = new User { Id = 2, IdByType = 1, Name = "Emilija", Surname = "Medic", Email="donor", Active = true, Token = null, Password = "APuucZwPYpx2awM5SRWZ55yMOqwvnKdxTyFmtxSskpMzABHMEILvphRla+B4hvTmhw==", UserType = UserType.DONOR };
            User u3 = new User { Id = 3, IdByType = 1, Email = "staff", Name = "Milan", Surname = "Miric", Active=true, Token=null,  Password= "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g==", UserType = UserType.STAFF };
           
            modelBuilder.Entity<User>().HasData(u1, u2, u3);

            base.OnModelCreating(modelBuilder);
        }
    }
}
