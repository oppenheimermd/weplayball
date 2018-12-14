using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class Fixture_Tbl_Correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Fixture");

            migrationBuilder.AddColumn<string>(
                name: "AwayTeamCode",
                table: "Fixture",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AwayTeamName",
                table: "Fixture",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamCode",
                table: "Fixture",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamName",
                table: "Fixture",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeamCode",
                table: "Fixture");

            migrationBuilder.DropColumn(
                name: "AwayTeamName",
                table: "Fixture");

            migrationBuilder.DropColumn(
                name: "HomeTeamCode",
                table: "Fixture");

            migrationBuilder.DropColumn(
                name: "HomeTeamName",
                table: "Fixture");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Team",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Fixture",
                rowVersion: true,
                nullable: true);
        }
    }
}
