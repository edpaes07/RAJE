using Microsoft.EntityFrameworkCore.Migrations;

namespace Raje.Migrations
{
    public partial class CampoImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemURL",
                table: "Series",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemURL",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemURL",
                table: "Filmes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemURL",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "ImagemURL",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "ImagemURL",
                table: "Filmes");
        }
    }
}
