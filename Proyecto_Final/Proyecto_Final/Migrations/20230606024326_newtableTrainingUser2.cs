using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class newtableTrainingUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingsUser_Trainings_TrainingId",
                table: "TrainingsUser");

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainingId",
                table: "TrainingsUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TrainingsUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TrainingsUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsUser_UserId_TrainingId",
                table: "TrainingsUser",
                columns: new[] { "UserId", "TrainingId" },
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingsUser_Trainings_TrainingId",
                table: "TrainingsUser",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingsUser_AspNetUsers_UserId1",
                table: "TrainingsUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingsUser_Trainings_TrainingId",
                table: "TrainingsUser");

            migrationBuilder.DropIndex(
                name: "IX_TrainingsUser_UserId_TrainingId",
                table: "TrainingsUser");

            migrationBuilder.DropIndex(
                name: "IX_TrainingsUser_UserId1",
                table: "TrainingsUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingsUser");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TrainingsUser");

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainingId",
                table: "TrainingsUser",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingsUser_Trainings_TrainingId",
                table: "TrainingsUser",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }
    }
}
