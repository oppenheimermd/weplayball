using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weplayball.Migrations
{
    public partial class Drop_tbl_rank_add_TeamStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rank");

            migrationBuilder.CreateTable(
                name: "TeamStat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamName = table.Column<string>(maxLength: 200, nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TeamCode = table.Column<string>(maxLength: 4, nullable: false),
                    SubDivisionId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesWon = table.Column<int>(nullable: false),
                    GamesLost = table.Column<int>(nullable: false),
                    BasketsFor = table.Column<int>(nullable: false),
                    BasketsAganist = table.Column<int>(nullable: false),
                    PointsDifference = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStat_SubDivision_SubDivisionId",
                        column: x => x.SubDivisionId,
                        principalTable: "SubDivision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStat_SubDivisionId",
                table: "TeamStat",
                column: "SubDivisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamStat");

            migrationBuilder.CreateTable(
                name: "Rank",
                columns: table => new
                {
                    RankEncoded = table.Column<string>(nullable: false),
                    GamesLost = table.Column<int>(nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesWon = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    SubDivisionId = table.Column<int>(nullable: false),
                    TeamCode = table.Column<string>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Rank_SubDivisionId",
                table: "Rank",
                column: "SubDivisionId");
        }
    }
}
