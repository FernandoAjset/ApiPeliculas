namespace Peliculas.Entidades
{
    public class CineOferta
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        //Por convención este campo sirve para crear la llave foranea (Relacion uno a uno)
        public int? CineId { get; set; }
    }
}
