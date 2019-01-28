using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class Instagram_tbl_Update_IsVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVideo",
                table: "InstagramItem",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVideo",
                table: "InstagramItem");
        }
    }
}
