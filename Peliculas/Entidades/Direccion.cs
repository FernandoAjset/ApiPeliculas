using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Peliculas.Entidades
{
    [Owned] //Propiedad de otra clase
    public class Direccion
    {
        [Required]
        public string Calle { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
    }
}
