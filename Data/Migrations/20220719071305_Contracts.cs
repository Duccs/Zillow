using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zillow.Data.Migrations
{
    public partial class Contracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateId",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Contract",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Contract",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "RealEstateID",
                table: "Contract",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_RealEstateID",
                table: "Contract",
                column: "RealEstateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_RealEstate_RealEstateID",
                table: "Contract",
                column: "RealEstateID",
                principalTable: "RealEstate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_RealEstate_RealEstateID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Contract_RealEstateID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "RealEstateID",
                table: "Contract");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateId",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Contract",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_RealEstate_RealEstateId",
                table: "Image",
                column: "RealEstateId",
                principalTable: "RealEstate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
