using Microsoft.EntityFrameworkCore.Migrations;

namespace Nextwo.Migrations
{
    public partial class nnnnnnnnnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RequestId",
                table: "Users",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RequestId",
                table: "Courses",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Requests_RequestId",
                table: "Courses",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Requests_RequestId",
                table: "Users",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Requests_RequestId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Requests_RequestId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RequestId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Courses_RequestId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Courses");
        }
    }
}
