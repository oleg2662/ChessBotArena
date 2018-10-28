using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BoardGame.Service.Data.Migrations
{
    public partial class ExtendingChessMovesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChessMoveType",
                table: "ChessMoves",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CastlingType",
                table: "ChessMoves",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CapturePositionColumn",
                table: "ChessMoves",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CapturePositionRow",
                table: "ChessMoves",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromoteTo",
                table: "ChessMoves",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChessMoveType",
                table: "ChessMoves");

            migrationBuilder.DropColumn(
                name: "CastlingType",
                table: "ChessMoves");

            migrationBuilder.DropColumn(
                name: "CapturePositionColumn",
                table: "ChessMoves");

            migrationBuilder.DropColumn(
                name: "CapturePositionRow",
                table: "ChessMoves");

            migrationBuilder.DropColumn(
                name: "PromoteTo",
                table: "ChessMoves");
        }
    }
}
