using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class Team_tbl_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Team",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasLogo",
                table: "Team",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Team",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "HasLogo",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Team");
        }
    }
}
