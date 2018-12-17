using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSourceFixture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataSourceDescription = table.Column<string>(maxLength: 300, nullable: false),
                    Url = table.Column<string>(maxLength: 300, nullable: false),
                    Division = table.Column<string>(nullable: false),
                    DivisionCode = table.Column<string>(maxLength: 4, nullable: false),
                    UrlHash = table.Column<string>(nullable: false),
                    ClassNameNode = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceFixture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceRanking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataSourceDescription = table.Column<string>(maxLength: 300, nullable: false),
                    Url = table.Column<string>(maxLength: 300, nullable: false),
                    Division = table.Column<string>(nullable: false),
                    DivisionCode = table.Column<string>(maxLength: 4, nullable: false),
                    UrlHash = table.Column<string>(nullable: false),
                    ClassNameNode = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceRanking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataSourceDescription = table.Column<string>(maxLength: 300, nullable: false),
                    Url = table.Column<string>(nullable: false),
                    Division = table.Column<string>(nullable: false),
                    DivisionCode = table.Column<string>(maxLength: 4, nullable: false),
                    UrlHash = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    ClassNameNode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivisionName = table.Column<string>(maxLength: 100, nullable: false),
                    DivisionCode = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubDivision",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubDivisionTitle = table.Column<string>(nullable: false),
                    SubDivisionCode = table.Column<string>(maxLength: 4, nullable: false),
                    DivisionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDivision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubDivision_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fixture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixtureDate = table.Column<DateTime>(nullable: false),
                    HomeTeamId = table.Column<int>(nullable: false),
                    HomeTeamName = table.Column<string>(nullable: false),
                    HomeTeamCode = table.Column<string>(nullable: false),
                    AwayTeamId = table.Column<int>(nullable: false),
                    AwayTeamName = table.Column<string>(nullable: false),
                    AwayTeamCode = table.Column<string>(nullable: false),
                    SubDivisionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fixture_SubDivision_SubDivisionId",
                        column: x => x.SubDivisionId,
                        principalTable: "SubDivision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    HomeTeamId = table.Column<int>(nullable: false),
                    HomeTeamName = table.Column<string>(nullable: false),
                    HomeTeamCode = table.Column<string>(nullable: false),
                    AwayTeamId = table.Column<int>(nullable: false),
                    AwayTeamName = table.Column<string>(nullable: false),
                    AwayTeamCode = table.Column<string>(nullable: false),
                    Score = table.Column<string>(nullable: false),
                    WinningTeamName = table.Column<string>(nullable: false),
                    WinningTeamCode = table.Column<string>(nullable: false),
                    SubDivisionId = table.Column<int>(nullable: false),
                    EncodedResult = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameResult_SubDivision_SubDivisionId",
                        column: x => x.SubDivisionId,
                        principalTable: "SubDivision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rank",
                columns: table => new
                {
                    RankEncoded = table.Column<string>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesWon = table.Column<int>(nullable: false),
                    GamesLost = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    SubDivisionId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TeamCode = table.Column<string>(nullable: false),
                    TeamName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.RankEncoded);
                    table.ForeignKey(
                        name: "FK_Rank_SubDivision_SubDivisionId",
                        column: x => x.SubDivisionId,
                        principalTable: "SubDivision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamName = table.Column<string>(maxLength: 200, nullable: false),
                    TeamCode = table.Column<string>(maxLength: 4, nullable: false),
                    SubDivisionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_SubDivision_SubDivisionId",
                        column: x => x.SubDivisionId,
                        principalTable: "SubDivision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixture_SubDivisionId",
                table: "Fixture",
                column: "SubDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResult_SubDivisionId",
                table: "GameResult",
                column: "SubDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rank_SubDivisionId",
                table: "Rank",
                column: "SubDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubDivision_DivisionId",
                table: "SubDivision",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_SubDivisionId",
                table: "Team",
                column: "SubDivisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSourceFixture");

            migrationBuilder.DropTable(
                name: "DataSourceRanking");

            migrationBuilder.DropTable(
                name: "DataSourceResult");

            migrationBuilder.DropTable(
                name: "Fixture");

            migrationBuilder.DropTable(
                name: "GameResult");

            migrationBuilder.DropTable(
                name: "Rank");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "SubDivision");

            migrationBuilder.DropTable(
                name: "Division");
        }
    }
}
