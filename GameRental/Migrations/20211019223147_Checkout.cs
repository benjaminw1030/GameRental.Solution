using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRental.Migrations
{
    public partial class Checkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copies",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "Rented",
                table: "Games",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rented",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
