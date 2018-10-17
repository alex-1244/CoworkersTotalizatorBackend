using Microsoft.EntityFrameworkCore.Migrations;

namespace CoworkersTotalizator.Dal.Migrations
{
    public partial class uniquecoworkername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coworkers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coworkers_Name",
                table: "Coworkers",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coworkers_Name",
                table: "Coworkers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coworkers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
