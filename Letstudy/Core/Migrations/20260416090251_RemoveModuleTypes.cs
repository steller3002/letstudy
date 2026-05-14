using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Letstudy.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveModuleTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "modules");

            migrationBuilder.CreateTable(
                name: "exercise_blocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exercise_blocks_blocks_Id",
                        column: x => x.Id,
                        principalTable: "blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_blocks_Given",
                columns: table => new
                {
                    ExerciseBlockId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_blocks_Given", x => new { x.ExerciseBlockId, x.Id });
                    table.ForeignKey(
                        name: "FK_exercise_blocks_Given_exercise_blocks_ExerciseBlockId",
                        column: x => x.ExerciseBlockId,
                        principalTable: "exercise_blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_blocks_Solution",
                columns: table => new
                {
                    ExerciseBlockId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_blocks_Solution", x => new { x.ExerciseBlockId, x.Id });
                    table.ForeignKey(
                        name: "FK_exercise_blocks_Solution_exercise_blocks_ExerciseBlockId",
                        column: x => x.ExerciseBlockId,
                        principalTable: "exercise_blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exercise_blocks_Given");

            migrationBuilder.DropTable(
                name: "exercise_blocks_Solution");

            migrationBuilder.DropTable(
                name: "exercise_blocks");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
