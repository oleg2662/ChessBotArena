using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BoardGame.Service.Data.Migrations
{
    public partial class DbRenamesToRemoveDbFromNamesStep1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChessGames_AspNetUsers_BlackPlayerId",
                table: "ChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ChessGames_AspNetUsers_InitiatedById",
                table: "ChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ChessGames_AspNetUsers_OpponentId",
                table: "ChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ChessGames_AspNetUsers_WhitePlayerId",
                table: "ChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_DbChessMove_ChessGames_DbChessGameId",
                table: "DbChessMove");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChessGames",
                table: "ChessGames");

            migrationBuilder.RenameTable(
                name: "ChessGames",
                newName: "DbChessGames");

            migrationBuilder.RenameIndex(
                name: "IX_ChessGames_WhitePlayerId",
                table: "DbChessGames",
                newName: "IX_DbChessGames_WhitePlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_ChessGames_OpponentId",
                table: "DbChessGames",
                newName: "IX_DbChessGames_OpponentId");

            migrationBuilder.RenameIndex(
                name: "IX_ChessGames_InitiatedById",
                table: "DbChessGames",
                newName: "IX_DbChessGames_InitiatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ChessGames_BlackPlayerId",
                table: "DbChessGames",
                newName: "IX_DbChessGames_BlackPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbChessGames",
                table: "DbChessGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbChessGames_AspNetUsers_BlackPlayerId",
                table: "DbChessGames",
                column: "BlackPlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbChessGames_AspNetUsers_InitiatedById",
                table: "DbChessGames",
                column: "InitiatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbChessGames_AspNetUsers_OpponentId",
                table: "DbChessGames",
                column: "OpponentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbChessGames_AspNetUsers_WhitePlayerId",
                table: "DbChessGames",
                column: "WhitePlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbChessMove_DbChessGames_DbChessGameId",
                table: "DbChessMove",
                column: "DbChessGameId",
                principalTable: "DbChessGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbChessGames_AspNetUsers_BlackPlayerId",
                table: "DbChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_DbChessGames_AspNetUsers_InitiatedById",
                table: "DbChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_DbChessGames_AspNetUsers_OpponentId",
                table: "DbChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_DbChessGames_AspNetUsers_WhitePlayerId",
                table: "DbChessGames");

            migrationBuilder.DropForeignKey(
                name: "FK_DbChessMove_DbChessGames_DbChessGameId",
                table: "DbChessMove");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbChessGames",
                table: "DbChessGames");

            migrationBuilder.RenameTable(
                name: "DbChessGames",
                newName: "ChessGames");

            migrationBuilder.RenameIndex(
                name: "IX_DbChessGames_WhitePlayerId",
                table: "ChessGames",
                newName: "IX_ChessGames_WhitePlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_DbChessGames_OpponentId",
                table: "ChessGames",
                newName: "IX_ChessGames_OpponentId");

            migrationBuilder.RenameIndex(
                name: "IX_DbChessGames_InitiatedById",
                table: "ChessGames",
                newName: "IX_ChessGames_InitiatedById");

            migrationBuilder.RenameIndex(
                name: "IX_DbChessGames_BlackPlayerId",
                table: "ChessGames",
                newName: "IX_ChessGames_BlackPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChessGames",
                table: "ChessGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGames_AspNetUsers_BlackPlayerId",
                table: "ChessGames",
                column: "BlackPlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGames_AspNetUsers_InitiatedById",
                table: "ChessGames",
                column: "InitiatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChessGames_AspNetUsers_OpponentId",
                table: "ChessGames",
                column: "OpponentId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_DbChessMove_ChessGames_DbChessGameId",
                table: "DbChessMove",
                column: "DbChessGameId",
                principalTable: "ChessGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
