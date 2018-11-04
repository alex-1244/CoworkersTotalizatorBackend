using Microsoft.EntityFrameworkCore.Migrations;

namespace CoworkersTotalizator.Dal.Migrations
{
    public partial class is_deleted_propertiesaddedtocoworkerandlottery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Lotteries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Coworkers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Lotteries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Coworkers");
        }
    }
}
