using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRental.Migrations
{
    public partial class Copies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rented",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "Copy",
                columns: table => new
                {
                    CopyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Rented = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copy", x => x.CopyId);
                    table.ForeignKey(
                        name: "FK_Copy_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Copy_GameId",
                table: "Copy",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Copy");

            migrationBuilder.AddColumn<bool>(
                name: "Rented",
                table: "Games",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
