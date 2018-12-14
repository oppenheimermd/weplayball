using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class SubDivision_Tbl_Correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SubDivision");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SubDivision",
                rowVersion: true,
                nullable: true);
        }
    }
}
