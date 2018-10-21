using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoworkersTotalizator.Dal.Migrations
{
    public partial class lottery_user_coworker_userBid_nav_properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lotteries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LotteryCoworker",
                columns: table => new
                {
                    LotteryId = table.Column<int>(nullable: false),
                    CoworkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryCoworker", x => new { x.LotteryId, x.CoworkerId });
                    table.ForeignKey(
                        name: "FK_LotteryCoworker_Coworkers_CoworkerId",
                        column: x => x.CoworkerId,
                        principalTable: "Coworkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotteryCoworker_Lotteries_LotteryId",
                        column: x => x.LotteryId,
                        principalTable: "Lotteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBid",
                columns: table => new
                {
                    LotteryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CoworkerId = table.Column<int>(nullable: false),
                    Bid = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBid", x => new { x.LotteryId, x.UserId, x.CoworkerId });
                    table.ForeignKey(
                        name: "FK_UserBid_Coworkers_CoworkerId",
                        column: x => x.CoworkerId,
                        principalTable: "Coworkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBid_Lotteries_LotteryId",
                        column: x => x.LotteryId,
                        principalTable: "Lotteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBid_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotteryCoworker_CoworkerId",
                table: "LotteryCoworker",
                column: "CoworkerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBid_CoworkerId",
                table: "UserBid",
                column: "CoworkerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBid_UserId",
                table: "UserBid",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotteryCoworker");

            migrationBuilder.DropTable(
                name: "UserBid");

            migrationBuilder.DropTable(
                name: "Lotteries");
        }
    }
}
