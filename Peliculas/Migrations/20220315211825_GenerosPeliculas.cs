using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class GenerosPeliculas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneroPelicula_Generos_GenerosIdentificador",
                table: "GeneroPelicula");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneroPelicula_Peliculas_PeliculasId",
                table: "GeneroPelicula");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneroPelicula",
                table: "GeneroPelicula");

            migrationBuilder.RenameTable(
                name: "GeneroPelicula",
                newName: "GenerosPeliculas");

            migrationBuilder.RenameIndex(
                name: "IX_GeneroPelicula_PeliculasId",
                table: "GenerosPeliculas",
                newName: "IX_GenerosPeliculas_PeliculasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenerosPeliculas",
                table: "GenerosPeliculas",
                columns: new[] { "GenerosIdentificador", "PeliculasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenerosPeliculas_Generos_GenerosIdentificador",
                table: "GenerosPeliculas",
                column: "GenerosIdentificador",
                principalTable: "Generos",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenerosPeliculas_Peliculas_PeliculasId",
                table: "GenerosPeliculas",
                column: "PeliculasId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenerosPeliculas_Generos_GenerosIdentificador",
                table: "GenerosPeliculas");

            migrationBuilder.DropForeignKey(
                name: "FK_GenerosPeliculas_Peliculas_PeliculasId",
                table: "GenerosPeliculas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenerosPeliculas",
                table: "GenerosPeliculas");

            migrationBuilder.RenameTable(
                name: "GenerosPeliculas",
                newName: "GeneroPelicula");

            migrationBuilder.RenameIndex(
                name: "IX_GenerosPeliculas_PeliculasId",
                table: "GeneroPelicula",
                newName: "IX_GeneroPelicula_PeliculasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneroPelicula",
                table: "GeneroPelicula",
                columns: new[] { "GenerosIdentificador", "PeliculasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroPelicula_Generos_GenerosIdentificador",
                table: "GeneroPelicula",
                column: "GenerosIdentificador",
                principalTable: "Generos",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneroPelicula_Peliculas_PeliculasId",
                table: "GeneroPelicula",
                column: "PeliculasId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
