using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Letstudy.Core.Migrations
{
    /// <inheritdoc />
    public partial class UsersListForBoardCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_boards_students_StudentId",
                table: "boards");

            migrationBuilder.DropIndex(
                name: "IX_boards_StudentId",
                table: "boards");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "boards");

            migrationBuilder.CreateTable(
                name: "BoardUser",
                columns: table => new
                {
                    BoardsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardUser", x => new { x.BoardsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BoardUser_boards_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_UsersId",
                table: "BoardUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardUser");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "boards",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_boards_StudentId",
                table: "boards",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_boards_students_StudentId",
                table: "boards",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id");
        }
    }
}
