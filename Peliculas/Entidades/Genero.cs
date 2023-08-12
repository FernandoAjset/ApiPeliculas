using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Peliculas.Entidades
{
    //[Table("TablaGeneros", Schema="peliculas")] para cambiar el nombre de la tabla creada en la BD
    //[Index(nameof(Nombre), IsUnique = true)] para configurar un indice sobre el campo Nombre, asi será unico
    public class Genero : EntidadAuditable
    {
        //[Key] Usando atributos
        public int Identificador { get; set; }
        //[StringLength(150)] Las dos opciones funcionan igual, son ejemplos del mismo caso
        //[MaxLength(150)]
        //[Required] Para que no sea nulo usando atributo
        //[Column("NombreGenero")] para cambiar el nombre del atributo de tabla en la BD
        [ConcurrencyCheck]
        public string Nombre { get; set; }
        public HashSet<Pelicula> Peliculas { get; set; }
        public bool Desactivado { get; set; }
    }
}
