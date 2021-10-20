﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRental.Migrations
{
    public partial class rentaldate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RentalDate",
                table: "Copies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalDate",
                table: "Copies");
        }
    }
}
