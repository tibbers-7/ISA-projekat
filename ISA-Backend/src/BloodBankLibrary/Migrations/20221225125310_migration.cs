using System;
using BloodBankLibrary.Core.Materials.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BloodBankLibrary.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_status", "scheduled,available,cancelled,completed")
                .Annotation("Npgsql:Enum:gender", "male,female,other")
                .Annotation("Npgsql:Enum:user_type", "donor,staff,admin");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StaffId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    DonorId = table.Column<int>(type: "integer", nullable: false),
                    CenterId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<AppointmentStatus>(type: "appointment_status", nullable: false),
                    ReportId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AvgScore = table.Column<double>(type: "double precision", nullable: false),
                    WorkTimeStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WorkTimeEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AddressJson = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    AddressJson = table.Column<string>(type: "jsonb", nullable: true),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Jmbg = table.Column<long>(type: "bigint", nullable: false),
                    Profession = table.Column<string>(type: "text", nullable: true),
                    Workplace = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<Gender>(type: "gender", nullable: false),
                    Strikes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DonorId = table.Column<int>(type: "integer", nullable: false),
                    Answers = table.Column<bool[]>(type: "boolean[]", nullable: true),
                    QuestionIds = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CenterId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Jmbg = table.Column<long>(type: "bigint", nullable: false),
                    AddressJson = table.Column<string>(type: "jsonb", nullable: true),
                    Gender = table.Column<Gender>(type: "gender", nullable: false),
                    Profession = table.Column<string>(type: "text", nullable: true),
                    Workplace = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdByType = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    UserType = table.Column<UserType>(type: "user_type", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Name", "Surname" },
                values: new object[] { 1, "admin", "Marko", "Dobrosavljevic" });

            migrationBuilder.InsertData(
                table: "BloodCenters",
                columns: new[] { "Id", "AddressJson", "AvgScore", "Description", "Name", "WorkTimeEnd", "WorkTimeStart" },
                values: new object[,]
                {
                    { 1, "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Futoska 62\"}", 4.9000000000000004, "Blood transfusion center.", "Center 1", new DateTime(1, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Bulevar Oslobodjenja 111\"}", 3.7000000000000002, "Blood transfusion center.", "Center 2", new DateTime(1, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Strazilovska 18\"}", 5.0, "Blood transfusion center.", "Center 3", new DateTime(1, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Vere Petrovic 1\"}", 4.2000000000000002, "Blood transfusion center.", "Center 4", new DateTime(1, 1, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "Id", "AddressJson", "Email", "Gender", "Jmbg", "Name", "PhoneNumber", "Profession", "Strikes", "Surname", "Workplace" },
                values: new object[] { 1, "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Bore Prodanovica 11\"}", "donor", Gender.FEMALE, 34242423565L, "Emilija", 381629448332L, "student", 0, "Medic", "Fakultet Tehnickih Nauka" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Text" },
                values: new object[,]
                {
                    { 8, "Have you ever had unsafe sexual intercourse with a person suffering from HIV?" },
                    { 7, "Have you ever consumed any type of opioids?" },
                    { 6, "Have you had any tattoos or piercings done in the last 6 months?" },
                    { 5, "Did you drink any alcohol in the last 6 hours?" },
                    { 3, "Do you currently feel healthy and rested enough to donate blood?" },
                    { 2, "Have you ever been rejected as a blood donor?" },
                    { 1, "Have you donated blood in the last 6 months?" },
                    { 4, "Have you eaten anything prior to your arrival to donate blood?" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "AddressJson", "CenterId", "Email", "Gender", "Jmbg", "Name", "PhoneNumber", "Profession", "Surname", "Workplace" },
                values: new object[] { 1, "{\"City\":\"Novi Sad\",\"Country\":\"Srbija\",\"StreetAddr\":\"Bore Prodanovica 11\"}", 1, "staff", Gender.MALE, 47387297437L, "Milan", 3816298437L, null, "Miric", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Email", "IdByType", "Name", "Password", "Surname", "Token", "UserType" },
                values: new object[,]
                {
                    { 2, true, "donor", 1, "Emilija", "APuucZwPYpx2awM5SRWZ55yMOqwvnKdxTyFmtxSskpMzABHMEILvphRla+B4hvTmhw==", "Medic", null, UserType.DONOR },
                    { 1, true, "admin", 1, "Marko", "AM/u63R1v9SxmknTfBDYIFJgB3+ABmOQZValIoEB0rsuGtKi4HhVbUca8lDFsZDRTA==", "Dobrosavljevic", null, UserType.ADMIN },
                    { 3, true, "staff", 1, "Milan", "AMnI1Ks4LwHaa8litjbGOhpvrAV/2e0IZsv6EXpkTMORSQ1GQ1nwiiSE7yEIKjdM9g==", "Miric", null, UserType.STAFF }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "BloodCenters");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
