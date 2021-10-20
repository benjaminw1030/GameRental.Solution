using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRental.Migrations
{
    public partial class copies2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copy_Games_GameId",
                table: "Copy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Copy",
                table: "Copy");

            migrationBuilder.RenameTable(
                name: "Copy",
                newName: "Copies");

            migrationBuilder.RenameIndex(
                name: "IX_Copy_GameId",
                table: "Copies",
                newName: "IX_Copies_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Copies",
                table: "Copies",
                column: "CopyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Games_GameId",
                table: "Copies",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Games_GameId",
                table: "Copies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Copies",
                table: "Copies");

            migrationBuilder.RenameTable(
                name: "Copies",
                newName: "Copy");

            migrationBuilder.RenameIndex(
                name: "IX_Copies_GameId",
                table: "Copy",
                newName: "IX_Copy_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Copy",
                table: "Copy",
                column: "CopyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copy_Games_GameId",
                table: "Copy",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
