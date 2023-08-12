using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Peliculas.Migraciones
{
    public partial class FuncionesDefinidasPorElUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE FUNCTION FacturaDetalleSuma
(
@FacturaId INT
)
RETURNS int
AS
BEGIN

--Declaracion de variables locales
DECLARE @suma INT

--Sentencias a realizar
SELECT @suma = SUM(precio)
FROM FacturaDetalles
WHERE FacturaId=@FacturaId

--Lo que debe retornar la función
RETURN @suma

END
");

            migrationBuilder.Sql(@"
CREATE FUNCTION FacturaDetallePromedio
(
@FacturaId INT
)
RETURNS decimal(18,2)
AS
BEGIN
--Declaracion de variables locales
DECLARE @promedio decimal(18,2)

--Sentencias a realizar
SELECT @promedio = AVG(Precio)
FROM FacturaDetalles
WHERE FacturaId=@FacturaId

--Lo que debe retornar la funcion
RETURN @promedio

END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION [dbo].[FacturaDetalleSuma]");
            migrationBuilder.Sql("DROP FUNCTION [dbo].[FacturaDetallePromedio]");
        }
    }
}
