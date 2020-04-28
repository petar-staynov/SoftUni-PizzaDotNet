using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDotNet.Data.Migrations
{
    public partial class OrderPrecalculatedPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPriceDiscounted",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPriceDiscounted",
                table: "Orders");
        }
    }
}
