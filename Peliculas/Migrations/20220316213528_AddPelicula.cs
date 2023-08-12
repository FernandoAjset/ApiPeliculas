using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class AddPelicula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Nombre", "Precio" },
                values: new object[] { 4, "The Matrix Resurrections", 5.9900000000000002 });

            migrationBuilder.InsertData(
                table: "PeliculasAlquilables",
                columns: new[] { "Id", "PeliculaId" },
                values: new object[] { 4, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PeliculasAlquilables",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
