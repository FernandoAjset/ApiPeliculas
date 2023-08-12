using Microsoft.EntityFrameworkCore;

namespace Peliculas.Entidades.Funciones
{
    public class Escalares
    {
        public static void RegistrarFunciones(ModelBuilder modelBuilder)
        {
            //registra una función en el modelBuilder del DBContext
            modelBuilder.HasDbFunction(() => FacturaDetallePromedio(0));
        }

        public static decimal FacturaDetallePromedio(int facturaId) //Método que representa la función escalar en la BD
        {
            return 0;
        }
    }
}
