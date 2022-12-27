﻿// <auto-generated />
using System;
using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BloodBankLibrary.Migrations
{
    [DbContext(typeof(BloodBankDbContext))]
    partial class BloodBankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasPostgresEnum(null, "appointment_status", new[] { "scheduled", "available", "cancelled", "completed" })
                .HasPostgresEnum(null, "gender", new[] { "male", "female", "other" })
                .HasPostgresEnum(null, "user_type", new[] { "donor", "staff", "admin" })
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BloodBankLibrary.Core.Admins.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin",
                            Name = "Marko",
                            Surname = "Dobrosavljevic"
                        });
                });

            modelBuilder.Entity("BloodBankLibrary.Core.Appointments.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CenterId")
                        .HasColumnType("integer");

                    b.Property<int>("DonorId")
                        .HasColumnType("integer");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<byte[]>("QrCode")
                        .HasColumnType("bytea");

                    b.Property<int?>("ReportId")
                        .HasColumnType("integer");

                    b.Property<int>("StaffId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<AppointmentStatus>("Status")
                        .HasColumnType("appointment_status");

                    b.HasKey("Id");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("BloodBankLibrary.Core.Centers.BloodCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressJson")
                        .HasColumnType("jsonb");

                    b.Property<double>("AvgScore")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("WorkTimeEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("WorkTimeStart")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("BloodCenters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressJson = "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Futoska 62\"}",
                            AvgScore = 4.9000000000000004,
                            Description = "Blood transfusion center.",
                            Name = "Center 1",
                            WorkTimeEnd = new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTimeStart = new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            AddressJson = "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Bulevar Oslobodjenja 111\"}",
                            AvgScore = 3.7000000000000002,
                            Description = "Blood transfusion center.",
                            Name = "Center 2",
                            WorkTimeEnd = new DateTime(1, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTimeStart = new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            AddressJson = "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Strazilovska 18\"}",
                            AvgScore = 5.0,
                            Description = "Blood transfusion center.",
                            Name = "Center 3",
                            WorkTimeEnd = new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTimeStart = new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            AddressJson = "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Vere Petrovic 1\"}",
                            AvgScore = 4.2000000000000002,
                            Description = "Blood transfusion center.",
                            Name = "Center 4",
                            WorkTimeEnd = new DateTime(1, 1, 1, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTimeStart = new DateTime(1, 1, 1, 13, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("BloodBankLibrary.Core.Donors.Donor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressJson")
                        .HasColumnType("jsonb");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Gender>("Gender")
                        .HasColumnType("gender");

                    b.Property<long>("Jmbg")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Profession")
                        .HasColumnType("text");

                    b.Property<int>("Strikes")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Workplace")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Donors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressJson = "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Bore Prodanovica 11\"}",
                            Email = "donor",
                            Gender = Gender.FEMALE,
                            Jmbg = 34242423565L,
                            Name = "Emilija",
                            PhoneNumber = 381629448332L,
                            Profession = "student",
                            Strikes = 0,
                            Surname = "Medic",
                            Workplace = "Fakultet Tehnickih Nauka"
                        });
                });

            modelBuilder.Entity("BloodBankLibrary.Core.Forms.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool[]>("Answers")
                        .HasColumnType("boolean[]");

                    b.Property<int>("DonorId")
                        .HasColumnType("integer");

                    b.Property<int[]>("QuestionIds")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("BloodBankLibrary.Core.Forms.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Text = "Have you donated blood in the last 6 months?"
                        },
                        new
                        {
                            Id = 2,
                            Text = "Have you ever been rejected as a blood donor?"
                        },
                        new
                        {
                            Id = 3,
                            Text = "Do you currently feel healthy and rested enough to donate blood?"
                        },
                        new
                        {
                            Id = 4,
                            Text = "Have you eaten anything prior to your arrival to donate blood?"
                        },
                        new
                        {
                            Id = 5,
                            Text = "Did you drink any alcohol in the last 6 hours?"
                        },
                        new
                        {
                            Id = 6,
                            Text = "Have you had any tattoos or piercings done in the last 6 months?"
                        },
                        new
                        {
                            Id = 7,
                            Text = "Have you ever consumed any type of opioids?"
                        },
                        new
                        {
                            Id = 8,
                            Text = "Have you ever had unsafe sexual intercourse with a person suffering from HIV?"
                        });
                });

            modelBuilder.Entity("BloodBankLibrary.Core.Staffs.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AddressJson")
                        .HasColumnType("jsonb");

                    b.Property<int>("CenterId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Gender>("Gender")
                        .HasColumnType("gender");

                    b.Property<long>("Jmbg")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Profession")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Workplace")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Staff");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressJson = "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Bore Prodanovica 11\"}",
                            CenterId = 1,
                            Email = "staff",
                            Gender = Gender.MALE,
                            Jmbg = 47387297437L,
                            Name = "Milan",
                            PhoneNumber = 3816298437L,
                            Surname = "Miric"
                        });
                });

            modelBuilder.Entity("BloodBankLibrary.Core.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("IdByType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<UserType>("UserType")
                        .HasColumnType("user_type");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            Email = "admin",
                            IdByType = 1,
                            Name = "Marko",
                            Password = "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==",
                            Surname = "Dobrosavljevic",
                            UserType = UserType.ADMIN
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            Email = "donor",
                            IdByType = 1,
                            Name = "Emilija",
                            Password = "APuucZwPYpx2awM5SRWZ55yMOqwvnKdxTyFmtxSskpMzABHMEILvphRla+B4hvTmhw==",
                            Surname = "Medic",
                            UserType = UserType.DONOR
                        },
                        new
                        {
                            Id = 3,
                            Active = true,
                            Email = "staff",
                            IdByType = 1,
                            Name = "Milan",
                            Password = "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g==",
                            Surname = "Miric",
                            UserType = UserType.STAFF
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
