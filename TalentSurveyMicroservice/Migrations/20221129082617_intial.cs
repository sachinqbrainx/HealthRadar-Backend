using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentSurvey.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TalentSurveyTable",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentSurveyTable", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ResponsedAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RespondedAnswer = table.Column<string>(type: "text", nullable: false),
                    TalentSurveyCommandModelUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsedAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponsedAnswer_TalentSurveyTable_TalentSurveyCommandModelU~",
                        column: x => x.TalentSurveyCommandModelUserId,
                        principalTable: "TalentSurveyTable",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResponsedAnswer_TalentSurveyCommandModelUserId",
                table: "ResponsedAnswer",
                column: "TalentSurveyCommandModelUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponsedAnswer");

            migrationBuilder.DropTable(
                name: "TalentSurveyTable");
        }
    }
}
