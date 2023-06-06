using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class newtableTrainingUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingsUser_AspNetUsers_UserId",
                table: "TrainingsUser");

            migrationBuilder.DropIndex(
                name: "IX_TrainingsUser_UserId",
                table: "TrainingsUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingsUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TrainingsUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsUser_UserId",
                table: "TrainingsUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingsUser_AspNetUsers_UserId",
                table: "TrainingsUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
