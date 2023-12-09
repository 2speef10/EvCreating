using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvCreating.Migrations
{
    /// <inheritdoc />
    public partial class EvaluatieEventMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventEvaluation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactieDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Waardering = table.Column<int>(type: "int", nullable: false),
                    Inhoud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeselecteerdEvenementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEvaluation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventEvaluation_Event_GeselecteerdEvenementId",
                        column: x => x.GeselecteerdEvenementId,
                        principalTable: "Event",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventEvaluation_GeselecteerdEvenementId",
                table: "EventEvaluation",
                column: "GeselecteerdEvenementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEvaluation");
        }
    }
}
