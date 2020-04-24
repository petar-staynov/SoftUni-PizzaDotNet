using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDotNet.Data.Migrations
{
    public partial class AddressPersonalName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "Addresses");
        }
    }
}
