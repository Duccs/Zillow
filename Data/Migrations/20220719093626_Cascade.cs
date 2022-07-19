using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zillow.Data.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_Category_CategoryId",
                table: "RealEstate");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_Category_CategoryId",
                table: "RealEstate",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_Category_CategoryId",
                table: "RealEstate");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_Category_CategoryId",
                table: "RealEstate",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
