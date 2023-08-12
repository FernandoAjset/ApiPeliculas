using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migraciones
{
    public partial class TVF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE FUNCTION PeliculaConConteos
(
@peliculaId int
)
RETURNS TABLE
AS
RETURN
(
SELECT Id,Titulo,
(SELECT COUNT(*) 
FROM GenerosPeliculas
WHERE PeliculasId=Peliculas.Id) AS CantidadGeneros,
(SELECT COUNT(DISTINCT CineIdentificador) 
FROM PeliculaSalaDeCine
INNER JOIN SalasDeCines
ON SalasDeCines.Id=PeliculaSalaDeCine.SalasDeCineId
WHERE PeliculasId=Peliculas.Id) AS CantidadCines,
(SELECT COUNT(*) 
FROM PeliculasActores
WHERE PeliculaId=Peliculas.Id) AS CantidadActores
FROM Peliculas
WHERE Id=@peliculaId
)"
);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION [dbo].[PeliculaConConteos]");
        }
    }
}
