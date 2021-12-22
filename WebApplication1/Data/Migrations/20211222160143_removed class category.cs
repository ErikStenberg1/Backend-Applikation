using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class removedclasscategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Category_CategoryID",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Movie_CategoryID",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Movie",
                newName: "ReleaseYear");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Movie",
                newName: "CategoryID");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_CategoryID",
                table: "Movie",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Category_CategoryID",
                table: "Movie",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
