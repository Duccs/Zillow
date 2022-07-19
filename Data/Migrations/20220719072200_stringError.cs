using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zillow.Data.Migrations
{
    public partial class stringError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_RealEstate_RealEstateID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressId1",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_AddressId1",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AddressId1",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "RealEstateID",
                table: "Contract",
                newName: "RealEstateId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_RealEstateID",
                table: "Contract",
                newName: "IX_Contract_RealEstateId");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Customer",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                column: "AddressId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_RealEstate_RealEstateId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_AddressId",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "RealEstateId",
                table: "Contract",
                newName: "RealEstateID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_RealEstateId",
                table: "Contract",
                newName: "IX_Contract_RealEstateID");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId1",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId1",
                table: "Customer",
                column: "AddressId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_RealEstate_RealEstateID",
                table: "Contract",
                column: "RealEstateID",
                principalTable: "RealEstate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AddressId1",
                table: "Customer",
                column: "AddressId1",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
