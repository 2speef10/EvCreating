using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvCreating.Migrations
{
    /// <inheritdoc />
    public partial class GlobalsEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EvCreatingUser",
                table: "EventEvaluation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvCreatingUserId",
                table: "EventEvaluation",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvCreatingUser",
                table: "EventEvaluation");

            migrationBuilder.DropColumn(
                name: "EvCreatingUserId",
                table: "EventEvaluation");
        }
    }
}
