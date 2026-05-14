using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Letstudy.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_users_Id",
                        column: x => x.Id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tutors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tutors_users_Id",
                        column: x => x.Id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_boards_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_boards_tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTutor",
                columns: table => new
                {
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TutorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTutor", x => new { x.StudentsId, x.TutorsId });
                    table.ForeignKey(
                        name: "FK_StudentTutor_students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTutor_tutors_TutorsId",
                        column: x => x.TutorsId,
                        principalTable: "tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "access_keys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_keys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_access_keys_boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_access_keys_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_modules_boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "module_items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleType = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_module_items_modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_keys_BoardId",
                table: "access_keys",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_access_keys_StudentId",
                table: "access_keys",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_boards_StudentId",
                table: "boards",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_boards_TutorId",
                table: "boards",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_module_items_ModuleId",
                table: "module_items",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_modules_BoardId",
                table: "modules",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTutor_TutorsId",
                table: "StudentTutor",
                column: "TutorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_keys");

            migrationBuilder.DropTable(
                name: "module_items");

            migrationBuilder.DropTable(
                name: "StudentTutor");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "tutors");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
