using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Letstudy.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddIncludeInVariantField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeInVariant",
                table: "exercise_blocks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeInVariant",
                table: "exercise_blocks");
        }
    }
}
