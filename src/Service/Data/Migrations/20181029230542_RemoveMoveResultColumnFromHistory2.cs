using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BoardGame.Service.Data.Migrations
{
    public partial class RemoveMoveResultColumnFromHistory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChessMoveResult",
                table: "ChessMoves");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChessMoveResult",
                table: "ChessMoves",
                nullable: false,
                defaultValue: 0);
        }
    }
}
