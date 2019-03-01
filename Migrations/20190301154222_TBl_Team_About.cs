using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class TBl_Team_About : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Team",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Team");
        }
    }
}
