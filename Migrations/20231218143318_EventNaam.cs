using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvCreating.Migrations
{
    /// <inheritdoc />
    public partial class EventNaam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEvaluation_Event_GeselecteerdEvenementId",
                table: "EventEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_EventEvaluation_GeselecteerdEvenementId",
                table: "EventEvaluation");

            migrationBuilder.AddColumn<string>(
                name: "EventNaam",
                table: "EventEvaluation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventNaam",
                table: "EventEvaluation");

            migrationBuilder.CreateIndex(
                name: "IX_EventEvaluation_GeselecteerdEvenementId",
                table: "EventEvaluation",
                column: "GeselecteerdEvenementId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEvaluation_Event_GeselecteerdEvenementId",
                table: "EventEvaluation",
                column: "GeselecteerdEvenementId",
                principalTable: "Event",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
