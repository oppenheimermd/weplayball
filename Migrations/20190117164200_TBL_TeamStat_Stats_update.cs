using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class TBL_TeamStat_Stats_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasketsPerGame",
                table: "TeamStat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LossPercentage",
                table: "TeamStat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WPyth",
                table: "TeamStat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WinLossPercent",
                table: "TeamStat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WinPercentage",
                table: "TeamStat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WinsOver500",
                table: "TeamStat",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketsPerGame",
                table: "TeamStat");

            migrationBuilder.DropColumn(
                name: "LossPercentage",
                table: "TeamStat");

            migrationBuilder.DropColumn(
                name: "WPyth",
                table: "TeamStat");

            migrationBuilder.DropColumn(
                name: "WinLossPercent",
                table: "TeamStat");

            migrationBuilder.DropColumn(
                name: "WinPercentage",
                table: "TeamStat");

            migrationBuilder.DropColumn(
                name: "WinsOver500",
                table: "TeamStat");
        }
    }
}
