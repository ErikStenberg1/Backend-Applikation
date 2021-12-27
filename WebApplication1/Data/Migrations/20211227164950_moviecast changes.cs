using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class moviecastchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCasts_Actor_ActorID",
                table: "MovieCasts");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCasts_Movie_MovieID",
                table: "MovieCasts");

            migrationBuilder.AlterColumn<int>(
                name: "MovieID",
                table: "MovieCasts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActorID",
                table: "MovieCasts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCasts_Actor_ActorID",
                table: "MovieCasts",
                column: "ActorID",
                principalTable: "Actor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCasts_Movie_MovieID",
                table: "MovieCasts",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCasts_Actor_ActorID",
                table: "MovieCasts");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCasts_Movie_MovieID",
                table: "MovieCasts");

            migrationBuilder.AlterColumn<int>(
                name: "MovieID",
                table: "MovieCasts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ActorID",
                table: "MovieCasts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCasts_Actor_ActorID",
                table: "MovieCasts",
                column: "ActorID",
                principalTable: "Actor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCasts_Movie_MovieID",
                table: "MovieCasts",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
