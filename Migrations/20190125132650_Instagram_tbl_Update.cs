using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class Instagram_tbl_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "InstagramItem",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filename",
                table: "InstagramItem");
        }
    }
}
