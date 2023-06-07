using Microsoft.EntityFrameworkCore;
using Npgsql;

using BloodBankAPI.Model;
using BloodBankAPI.Materials.Enums;

namespace BloodBankAPI.Settings
{
    public class BloodBankDbContext : DbContext
    {
        //virtual?
        public DbSet<BloodCenter> BloodCenters { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Admin> Admins { set; get; }
        public DbSet<CenterAddress> CenterAddresses { get; set; }

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

            modelBuilder.Entity<Account>().ToTable("accounts");
            modelBuilder.Entity<Donor>().ToTable("donors");
            modelBuilder.Entity<Staff>().ToTable("staff");
            modelBuilder.Entity<Admin>().ToTable("admins");

            modelBuilder.Entity<BloodCenter>().HasOne(c => c.CenterAddress).WithOne(a => a.BloodCenter).HasForeignKey<CenterAddress>(a => a.CenterId);

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

         /*
            Account acc1 = new Account(){Id=1, Name = "Emilija", Surname = "Medic", Gender = Gender.FEMALE, Email = "donor@gmail.com", IsActive=true, Token = null, Password = "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==",  UserType= UserType.DONOR};
            Account acc2 = new Account(){Id=2, Name = "Marko", Surname = "Dobrosavljevic", Gender = Gender.FEMALE, Email = "admin@gmail.com", IsActive=true, Token = null, Password = "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==", UserType = UserType.ADMIN};
            Account acc3 = new Account(){Id=3, Name = "Milan", Surname = "Miric", Gender = Gender.MALE, Email = "staff@gmail.com", IsActive=true, Token = null, Password = "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==", UserType = UserType.STAFF};
            modelBuilder.Entity<Account>().HasData(acc1, acc2, acc3);

            Donor d = new Donor() { Id = 1, AccountId=1, Jmbg = 34242423565, AddressString = "Ise Bajica 1,Novi Sad,Srbija", PhoneNumber = 381629448332, Profession = "student", Workplace = "Fakultet Tehnickih Nauka", Strikes = 0 };
            Staff s = new Staff() { Id = 1, AccountId = 3, CenterId = 1};

            modelBuilder.Entity<Donor>().HasData(d);
            modelBuilder.Entity<Staff>().HasData(s);
           */
            base.OnModelCreating(modelBuilder);
        }
    }
}
