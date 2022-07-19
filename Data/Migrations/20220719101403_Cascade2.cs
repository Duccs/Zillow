using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zillow.Data.Migrations
{
    public partial class Cascade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_RealEstate_RealEstateId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                table: "Contract",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_RealEstate_RealEstateId",
                table: "Contract",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_RealEstate_RealEstateId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                table: "Contract",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_RealEstate_RealEstateId",
                table: "Contract",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstate_Address_AddressId",
                table: "RealEstate",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
