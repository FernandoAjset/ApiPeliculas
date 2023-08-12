using Microsoft.EntityFrameworkCore;

namespace Peliculas.Entidades.SinLlave
{
    //[Keyless] definir una entidad sin llave primaria
    public class CineSinUbicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
