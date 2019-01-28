using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class Instagram_tbl_Update_Correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "InstagramItem",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "InstagramItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
