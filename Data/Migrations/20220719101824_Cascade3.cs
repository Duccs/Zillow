using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zillow.Data.Migrations
{
    public partial class Cascade3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
