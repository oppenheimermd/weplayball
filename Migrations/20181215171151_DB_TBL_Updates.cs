using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class DB_TBL_Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Rank");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DataSourceResult");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DataSourceRanking");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "DataSourceFixture");

            migrationBuilder.RenameColumn(
                name: "HashedResult",
                table: "GameResult",
                newName: "WinningTeamName");

            migrationBuilder.AddColumn<string>(
                name: "AwayTeamCode",
                table: "GameResult",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AwayTeamName",
                table: "GameResult",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EncodedResult",
                table: "GameResult",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamCode",
                table: "GameResult",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeamName",
                table: "GameResult",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WinningTeamCode",
                table: "GameResult",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "DataSourceResult",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Division",
                table: "DataSourceResult",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeamCode",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "AwayTeamName",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "EncodedResult",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "HomeTeamCode",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "HomeTeamName",
                table: "GameResult");

            migrationBuilder.DropColumn(
                name: "WinningTeamCode",
                table: "GameResult");

            migrationBuilder.RenameColumn(
                name: "WinningTeamName",
                table: "GameResult",
                newName: "HashedResult");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Rank",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "GameResult",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "GameResult",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "DataSourceResult",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Division",
                table: "DataSourceResult",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DataSourceResult",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DataSourceRanking",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "DataSourceFixture",
                rowVersion: true,
                nullable: true);
        }
    }
}
