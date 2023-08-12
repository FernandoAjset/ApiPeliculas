using Microsoft.EntityFrameworkCore;

namespace Peliculas.Entidades
{
    public abstract class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Precision(18, 2)]
        public double Precio { get; set; }
    }
}
