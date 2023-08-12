using System.ComponentModel.DataAnnotations.Schema;

namespace Peliculas.Entidades
{
    public class SalaDeCine: IId
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public int CineIdentificador { get; set; }
        [ForeignKey(nameof(CineIdentificador))] //Configurando una llave foranea, saliendo de la convención
        public Cine Cine { get; set; }
        public TipoSalaDeCine TipoSalaDeCine { get; set; }
        public HashSet<Pelicula> Peliculas { get; set; }
        public Moneda Moneda { get; set; }
    }
}
