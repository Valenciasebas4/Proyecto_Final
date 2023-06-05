using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class edittabletraininguser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainingsUser_UserId_TrainingId",
                table: "TrainingsUser");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsUser_UserId",
                table: "TrainingsUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainingsUser_UserId",
                table: "TrainingsUser");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsUser_UserId_TrainingId",
                table: "TrainingsUser",
                columns: new[] { "UserId", "TrainingId" },
                unique: true,
                filter: "[UserId] IS NOT NULL AND [TrainingId] IS NOT NULL");
        }
    }
}
