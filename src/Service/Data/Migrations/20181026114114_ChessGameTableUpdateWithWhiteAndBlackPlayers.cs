using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BoardGame.Service.Data.Migrations
{
    public partial class ChessGameTableUpdateWithWhiteAndBlackPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlackPlayerId",
                table: "ChessGames",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhitePlayerId",
                table: "ChessGames",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChessGames_BlackPlayerId",
                table: "ChessGames",
                column: "BlackPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChessGames_WhitePlayerId",
                table: "ChessGames",
                column: "WhitePlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGames_AspNetUsers_BlackPlayerId",
                table: "ChessGames",
                column: "BlackPlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGames_AspNetUsers_WhitePlayerId",
                table: "ChessGames",
                column: "WhitePlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessGames_AspNetUsers_BlackPlayerId",
                table: "ChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ChessGames_AspNetUsers_WhitePlayerId",
                table: "ChessGames");

            migrationBuilder.DropIndex(
                name: "IX_ChessGames_BlackPlayerId",
                table: "ChessGames");

            migrationBuilder.DropIndex(
                name: "IX_ChessGames_WhitePlayerId",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "BlackPlayerId",
                table: "ChessGames");

            migrationBuilder.DropColumn(
                name: "WhitePlayerId",
                table: "ChessGames");
        }
    }
}
