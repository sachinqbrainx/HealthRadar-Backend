using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DecisionMakingSurveyQuestion",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionMakingSurveyQuestion", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "TalentSurveyQuestion",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentSurveyQuestion", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "DecisionMakingSurveyOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    DecisionMakingSurveyCommandModelQuestionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionMakingSurveyOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecisionMakingSurveyOptions_DecisionMakingSurveyQuestion_De~",
                        column: x => x.DecisionMakingSurveyCommandModelQuestionId,
                        principalTable: "DecisionMakingSurveyQuestion",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "TalentSurveyOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    TalentSurveyCommandModelQuestionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentSurveyOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TalentSurveyOptions_TalentSurveyQuestion_TalentSurveyComman~",
                        column: x => x.TalentSurveyCommandModelQuestionId,
                        principalTable: "TalentSurveyQuestion",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DecisionMakingSurveyOptions_DecisionMakingSurveyCommandMode~",
                table: "DecisionMakingSurveyOptions",
                column: "DecisionMakingSurveyCommandModelQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TalentSurveyOptions_TalentSurveyCommandModelQuestionId",
                table: "TalentSurveyOptions",
                column: "TalentSurveyCommandModelQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecisionMakingSurveyOptions");

            migrationBuilder.DropTable(
                name: "TalentSurveyOptions");

            migrationBuilder.DropTable(
                name: "DecisionMakingSurveyQuestion");

            migrationBuilder.DropTable(
                name: "TalentSurveyQuestion");
        }
    }
}
