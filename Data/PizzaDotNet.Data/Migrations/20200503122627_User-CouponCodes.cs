using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDotNet.Data.Migrations
{
    public partial class UserCouponCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CouponCodes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponCodes_UserId",
                table: "CouponCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CouponCodes_AspNetUsers_UserId",
                table: "CouponCodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CouponCodes_AspNetUsers_UserId",
                table: "CouponCodes");

            migrationBuilder.DropIndex(
                name: "IX_CouponCodes_UserId",
                table: "CouponCodes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CouponCodes");
        }
    }
}
