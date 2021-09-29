using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class cillFKReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Categories_CategoryId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_CategoryId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Receipts");

            migrationBuilder.AddColumn<int>(
                name: "CategoriesId",
                table: "Receipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CategoriesId",
                table: "Receipts",
                column: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Categories_CategoriesId",
                table: "Receipts",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Categories_CategoriesId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_CategoriesId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Receipts");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CategoryId",
                table: "Receipts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Categories_CategoryId",
                table: "Receipts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
