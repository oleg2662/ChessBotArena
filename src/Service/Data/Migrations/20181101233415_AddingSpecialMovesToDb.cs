using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BoardGame.Service.Data.Migrations
{
    public partial class AddingSpecialMovesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChessPiece",
                table: "ChessMoves");

            migrationBuilder.DropColumn(
                name: "IsCaptureMove",
                table: "ChessMoves");

            migrationBuilder.AlterColumn<int>(
                name: "ToRow",
                table: "ChessMoves",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FromRow",
                table: "ChessMoves",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ToRow",
                table: "ChessMoves",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromRow",
                table: "ChessMoves",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChessPiece",
                table: "ChessMoves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCaptureMove",
                table: "ChessMoves",
                nullable: false,
                defaultValue: false);
        }
    }
}
