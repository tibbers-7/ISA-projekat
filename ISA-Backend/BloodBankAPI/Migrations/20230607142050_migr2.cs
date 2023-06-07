using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBankAPI.Migrations
{
    public partial class migr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressString",
                table: "donors",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "donors",
                newName: "AddressString");
        }
    }
}
