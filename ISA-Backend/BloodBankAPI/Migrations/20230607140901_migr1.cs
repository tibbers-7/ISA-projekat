using System;
using BloodBankAPI.Materials.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BloodBankAPI.Migrations
{
    public partial class migr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_status", "scheduled,available,cancelled,completed")
                .Annotation("Npgsql:Enum:gender", "male,female,other")
                .Annotation("Npgsql:Enum:user_type", "donor,staff,admin");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<Gender>(type: "gender", nullable: false),
                    UserType = table.Column<UserType>(type: "user_type", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StaffId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    DonorId = table.Column<int>(type: "integer", nullable: false),
                    CenterId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<AppointmentStatus>(type: "appointment_status", nullable: false),
                    ReportId = table.Column<int>(type: "integer", nullable: true),
                    QrCode = table.Column<byte[]>(type: "bytea", nullable: true)
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AvgScore = table.Column<double>(type: "double precision", nullable: false),
                    WorkTimeStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkTimeEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AmountA = table.Column<int>(type: "integer", nullable: true),
                    AmountB = table.Column<int>(type: "integer", nullable: true),
                    AmountAB = table.Column<int>(type: "integer", nullable: true),
                    AmountO = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DonorId = table.Column<int>(type: "integer", nullable: false),
                    Answers = table.Column<bool[]>(type: "boolean[]", nullable: false),
                    QuestionIds = table.Column<int[]>(type: "integer[]", nullable: false)
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
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admins_accounts_Id",
                        column: x => x.Id,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "donors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Jmbg = table.Column<long>(type: "bigint", nullable: false),
                    Profession = table.Column<string>(type: "text", nullable: false),
                    Workplace = table.Column<string>(type: "text", nullable: false),
                    AddressString = table.Column<string>(type: "text", nullable: false),
                    Strikes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_donors_accounts_Id",
                        column: x => x.Id,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    StreetAddress = table.Column<string>(type: "text", nullable: false),
                    CenterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterAddresses_BloodCenters_CenterId",
                        column: x => x.CenterId,
                        principalTable: "BloodCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CenterId = table.Column<int>(type: "integer", nullable: false),
                    BloodCenterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staff_accounts_Id",
                        column: x => x.Id,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_staff_BloodCenters_BloodCenterId",
                        column: x => x.BloodCenterId,
                        principalTable: "BloodCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BloodCenters",
                columns: new[] { "Id", "AmountA", "AmountAB", "AmountB", "AmountO", "AvgScore", "Description", "Name", "WorkTimeEnd", "WorkTimeStart" },
                values: new object[,]
                {
                    { 1, null, null, null, null, 4.9000000000000004, "Blood transfusion center.", "Center 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, null, null, null, 3.7000000000000002, "Blood transfusion center.", "Center 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, null, null, null, 5.0, "Blood transfusion center.", "Center 3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, null, null, null, 4.2000000000000002, "Blood transfusion center.", "Center 4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Text" },
                values: new object[,]
                {
                    { 1, "Have you donated blood in the last 6 months?" },
                    { 2, "Have you ever been rejected as a blood donor?" },
                    { 3, "Do you currently feel healthy and rested enough to donate blood?" },
                    { 4, "Have you eaten anything prior to your arrival to donate blood?" },
                    { 5, "Did you drink any alcohol in the last 6 hours?" },
                    { 6, "Have you had any tattoos or piercings done in the last 6 months?" },
                    { 7, "Have you ever consumed any type of opioids?" },
                    { 8, "Have you ever had unsafe sexual intercourse with a person suffering from HIV?" }
                });

            migrationBuilder.InsertData(
                table: "CenterAddresses",
                columns: new[] { "Id", "CenterId", "City", "Country", "StreetAddress" },
                values: new object[,]
                {
                    { 1, 1, "Novi Sad", "Srbija", "Futoska 62" },
                    { 2, 2, "Novi Sad", "Srbija", "Bulevar Oslobodjenja 111" },
                    { 3, 3, "Novi Sad", "Srbija", "Strazilovska 18" },
                    { 4, 4, "Novi Sad", "Srbija", "Vere Petrovic 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterAddresses_CenterId",
                table: "CenterAddresses",
                column: "CenterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_staff_BloodCenterId",
                table: "staff",
                column: "BloodCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "CenterAddresses");

            migrationBuilder.DropTable(
                name: "donors");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "BloodCenters");
        }
    }
}
