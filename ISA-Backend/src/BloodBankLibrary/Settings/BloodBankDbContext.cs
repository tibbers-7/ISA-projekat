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
/*
            BloodCenter bc1 = new BloodCenter("Center 1", 1, "Futoska 62,Novi Sad,Srbija", "Blood transfusion center.", 8, 8, 18 );
            BloodCenter bc2 = new BloodCenter ( "Center 2", 2, "Bulevar Oslobodjenja 111,Novi Sad,Srbija", "Blood transfusion center.", 9, 7, 14 );
            BloodCenter bc3 = new BloodCenter ("Center 3", 3, "Strazilovska 18,Novi Sad,Srbija", "Blood transfusion center.", 7, 9, 16 );
            BloodCenter bc4 = new BloodCenter ("Center 4", 4, "Vere Petrovic 1,Novi Sad,Srbija", "Blood transfusion center.", 6, 9, 17 );
            modelBuilder.Entity<BloodCenter>().HasData(bc1,bc2,bc3,bc4);

            modelBuilder.Entity<Donor>().HasData(new Donor(1, "Anja", "Medic", new Address("Novi Sad", "Srbija", "Bore Prodanovica 11"), "anjaamedic@gmail.com", Gender.FEMALE, "1207997905465", "064865268",  "Data scientist",  0,  "MIT"));
            modelBuilder.Entity<Admin>().HasData(new Admin (1, "admin", "Admy", "Adminini" ));

            Staff s1 = new Staff { Id = 1, Email = "staff1", Name = "Milan", Surname = "Miric", CenterId = 1 };
            Staff s2 = new Staff { Id = 2, Email = "staff2", Name = "Ivana", Surname = "Negovanovic", CenterId = 1 };
            Staff s3 = new Staff { Id = 3, Email = "staff3", Name = "Ljubomir", Surname = "Suljakovic", CenterId = 1 };
            Staff s4 = new Staff { Id = 4, Email = "staff4", Name = "Ena", Surname = "Popov", CenterId = 2 };
            Staff s5 = new Staff { Id = 5, Email = "staff5", Name = "Goga", Surname = "Sekulic", CenterId = 2 };
            modelBuilder.Entity<Staff>().HasData(s1, s2, s3, s4, s5);

            User u1 = new User { Id = 1, Name = "Admy", Surname = "Adminini", Email = "admin", IdByType =1, Active=true, Password= "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==", Token=null, UserType= UserType.ADMIN };
            User u2 = new User { Id = 2, IdByType = 1, Name = "Anja", Surname = "Medic", Token = null, Active = true, Email = "anjaamedic@gmail.com", Password = "APuucZwPYpx2awM5SRWZ55yMOqwvnKdxTyFmtxSskpMzABHMEILvphRla+B4hvTmhw==", UserType = UserType.DONOR };
            User u3 = new User { Id = 3, IdByType = 1, Email = "staff1", Name = "Milan", Surname = "Miric", Active=true, Token=null, UserType= UserType.STAFF, Password= "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g==" };
            User u4 = new User { Id = 4, IdByType = 2, Email = "staff2", Name = "Ivana", Surname = "Negovanovic", Active = true, Token = null, UserType = UserType.STAFF, Password = "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g==" };
            User u5 = new User { Id = 5, IdByType = 3, Email = "staff3", Name = "Ljubomir", Surname = "Suljakovic", Active = true, Token = null, UserType = UserType.STAFF, Password = "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g==" };
            User u6 = new User { Id = 6, IdByType = 4, Email = "staff4", Name = "Ena", Surname = "Popov", Active = true, Token = null, UserType = UserType.STAFF, Password = "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g=="};
            User u7 = new User { Id = 7, IdByType = 5, Email = "staff5", Name = "Goga", Surname = "Sekulic", Active = true, Token = null, UserType = UserType.STAFF, Password = "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g=="};
            modelBuilder.Entity<User>().HasData(u1, u2, u3, u4, u5);
*/
            base.OnModelCreating(modelBuilder);
        }
    }
}
