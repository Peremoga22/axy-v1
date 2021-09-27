using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class addFildIsIncome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Incomes",
                table: "Prices",
                newName: "Income");

            migrationBuilder.RenameColumn(
                name: "Costs",
                table: "Prices",
                newName: "Cost");

            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "Prices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "Income",
                table: "Prices",
                newName: "Incomes");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Prices",
                newName: "Costs");
        }
    }
}
