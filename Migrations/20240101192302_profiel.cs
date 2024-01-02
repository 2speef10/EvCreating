using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvCreating.Migrations
{
    /// <inheritdoc />
    public partial class profiel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvCreatingUser",
                table: "EventEvaluation");

            migrationBuilder.AlterColumn<string>(
                name: "EvCreatingUserId",
                table: "EventEvaluation",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventEvaluation_EvCreatingUserId",
                table: "EventEvaluation",
                column: "EvCreatingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEvaluation_AspNetUsers_EvCreatingUserId",
                table: "EventEvaluation",
                column: "EvCreatingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEvaluation_AspNetUsers_EvCreatingUserId",
                table: "EventEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_EventEvaluation_EvCreatingUserId",
                table: "EventEvaluation");

            migrationBuilder.AlterColumn<string>(
                name: "EvCreatingUserId",
                table: "EventEvaluation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvCreatingUser",
                table: "EventEvaluation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
