using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BoardGame.Service.Data.Migrations
{
    public partial class ChessGameHistoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ChessGames",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DbChessMove",
                columns: table => new
                {
                    MoveId = table.Column<Guid>(nullable: false),
                    ChessMoveResult = table.Column<int>(nullable: false),
                    ChessPiece = table.Column<int>(nullable: false),
                    DbChessGameId = table.Column<Guid>(nullable: true),
                    FromColumn = table.Column<string>(nullable: true),
                    FromRow = table.Column<int>(nullable: false),
                    IsCaptureMove = table.Column<bool>(nullable: false),
                    Owner = table.Column<int>(nullable: false),
                    ToColumn = table.Column<string>(nullable: true),
                    ToRow = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbChessMove", x => x.MoveId);
                    table.ForeignKey(
                        name: "FK_DbChessMove_ChessGames_DbChessGameId",
                        column: x => x.DbChessGameId,
                        principalTable: "ChessGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbChessMove_DbChessGameId",
                table: "DbChessMove",
                column: "DbChessGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbChessMove");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ChessGames");
        }
    }
}
