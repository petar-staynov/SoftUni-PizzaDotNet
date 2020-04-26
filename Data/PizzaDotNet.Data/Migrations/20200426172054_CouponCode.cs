using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDotNet.Data.Migrations
{
    public partial class CouponCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CouponCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    DiscountPercent = table.Column<int>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponCodes_IsDeleted",
                table: "CouponCodes",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponCodes");
        }
    }
}
