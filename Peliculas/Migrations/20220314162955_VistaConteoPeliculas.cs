using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migrations
{
    public partial class VistaConteoPeliculas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW [dbo].[PeliculasConConteos]
AS
SELECT Id,Titulo,
(SELECT COUNT(*) FROM GeneroPelicula
WHERE PeliculasId=Peliculas.Id) AS CantidadGeneros,
(SELECT COUNT(DISTINCT CineId) FROM PeliculaSalaDeCine
INNER JOIN SalasDeCines
ON SalasDeCines.Id=PeliculaSalaDeCine.SalasDeCineId
WHERE PeliculasId=Peliculas.Id) AS CantidadCines,
(SELECT COUNT(*) FROM PeliculasActores
WHERE PeliculaId=Peliculas.Id) AS CantidadActores
FROM Peliculas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[PeliculasConConteos]");
        }
    }
}
