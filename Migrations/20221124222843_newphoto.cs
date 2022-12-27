using Microsoft.EntityFrameworkCore.Migrations;

namespace Nextwo.Migrations
{
    public partial class newphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SliderImg",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderImg",
                table: "Sliders");
        }
    }
}
