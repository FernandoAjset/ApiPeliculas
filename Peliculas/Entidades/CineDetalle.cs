using System.ComponentModel.DataAnnotations;

namespace Peliculas.Entidades
{
    public class CineDetalle
    {
        public int Id { get; set; }
        [Required]
        public string Historia { get; set; }
        public string Valores { get; set; }
        public string Mision { get; set; }
        public string CodigoDeEtica { get; set; }
        public Cine Cine { get; set; }
    }
}
