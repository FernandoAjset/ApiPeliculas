using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace Peliculas.Entidades
{
    public class Cine
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public Point Ubicacion { get; set; }
        public CineOferta CineOferta { get; set; }
        public CineDetalle CineDetalle { get; set; }
        public Direccion Direccion { get; set; }
        public ObservableCollection<SalaDeCine> SalasDeCine { get; set; }//Representa una relación de uno a muchos
    }
}
