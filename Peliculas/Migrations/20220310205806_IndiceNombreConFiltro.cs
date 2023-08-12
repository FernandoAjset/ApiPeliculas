using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class IndiceNombreConFiltro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Generos_Nombre",
                table: "Generos");

            migrationBuilder.CreateIndex(
                name: "IX_Generos_Nombre",
                table: "Generos",
                column: "Nombre",
                unique: true,
                filter: "Desactivado='false'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Generos_Nombre",
                table: "Generos");

            migrationBuilder.CreateIndex(
                name: "IX_Generos_Nombre",
                table: "Generos",
                column: "Nombre",
                unique: true);
        }
    }
}
