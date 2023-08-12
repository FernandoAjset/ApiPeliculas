using System.ComponentModel.DataAnnotations.Schema;

namespace Peliculas.Entidades.SinLlave
{
    public class PeliculasConConteos
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int CantidadGeneros { get; set; }
        public int CantidadCines { get; set; }
        public int CantidadActores { get; set; }
    }
}
