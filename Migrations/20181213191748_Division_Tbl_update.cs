using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class Division_Tbl_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Division");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Division",
                rowVersion: true,
                nullable: true);
        }
    }
}
