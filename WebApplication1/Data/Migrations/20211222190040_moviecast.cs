using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class moviecast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movie_MovieID",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_MovieID",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "Actor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "Actor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_MovieID",
                table: "Actor",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movie_MovieID",
                table: "Actor",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
