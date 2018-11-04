using Microsoft.EntityFrameworkCore.Migrations;

namespace CoworkersTotalizator.Dal.Migrations
{
    public partial class lottery_user_results : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotteryUserResults",
                columns: table => new
                {
                    LotteryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CoworkerId = table.Column<int>(nullable: false),
                    Gain = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryUserResults", x => new { x.LotteryId, x.UserId, x.CoworkerId });
                    table.ForeignKey(
                        name: "FK_LotteryUserResults_Coworkers_CoworkerId",
                        column: x => x.CoworkerId,
                        principalTable: "Coworkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotteryUserResults_Lotteries_LotteryId",
                        column: x => x.LotteryId,
                        principalTable: "Lotteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotteryUserResults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotteryUserResults_CoworkerId",
                table: "LotteryUserResults",
                column: "CoworkerId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryUserResults_UserId",
                table: "LotteryUserResults",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotteryUserResults");
        }
    }
}
