using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDotNet.Data.Migrations
{
    public partial class CouponCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPriceDiscounted",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrdersProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouponCodeId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CouponCodeString",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DiscountPercent",
                table: "Orders",
                nullable: false,
                defaultValue: 0f);

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
                    Code = table.Column<string>(maxLength: 6, nullable: true),
                    DiscountPercent = table.Column<int>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponCodeId",
                table: "Orders",
                column: "CouponCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponCodes_IsDeleted",
                table: "CouponCodes",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CouponCodes_CouponCodeId",
                table: "Orders",
                column: "CouponCodeId",
                principalTable: "CouponCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CouponCodes_CouponCodeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CouponCodes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CouponCodeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrdersProducts");

            migrationBuilder.DropColumn(
                name: "CouponCodeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CouponCodeString",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrdersProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPriceDiscounted",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
