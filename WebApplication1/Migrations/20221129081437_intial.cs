using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecisionMakingSurvey.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DecisionMakingSurveyTable",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionMakingSurveyTable", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ResponsedAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RespondedAnswer = table.Column<string>(type: "text", nullable: false),
                    DecisionMakingSurveyCommandModelUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsedAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponsedAnswer_DecisionMakingSurveyTable_DecisionMakingSur~",
                        column: x => x.DecisionMakingSurveyCommandModelUserId,
                        principalTable: "DecisionMakingSurveyTable",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResponsedAnswer_DecisionMakingSurveyCommandModelUserId",
                table: "ResponsedAnswer",
                column: "DecisionMakingSurveyCommandModelUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponsedAnswer");

            migrationBuilder.DropTable(
                name: "DecisionMakingSurveyTable");
        }
    }
}
