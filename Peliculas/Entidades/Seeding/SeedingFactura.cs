using Microsoft.EntityFrameworkCore;

namespace Peliculas.Entidades.Seeding
{
    public class SeedingFactura
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //Factura y su detalle
            var factura1 = new Factura()
            {
                Id = 4, FechaCreacion = new DateTime(2022,01,03)
            };
            var detalle1 = new List<FacturaDetalle>()
            {
                new FacturaDetalle()
                {
                    Id=6,FacturaId=factura1.Id, Precio=40,Producto="Combo nachos"
                },
                new FacturaDetalle()
                {
                    Id=7,FacturaId=factura1.Id, Precio=60,Producto="Combo especial"
                },
                new FacturaDetalle()
                {
                    Id=8,FacturaId=factura1.Id, Precio=55,Producto="Combo dulces"
                }
            };
            modelBuilder.Entity<Factura>().HasData(factura1);
            modelBuilder.Entity<FacturaDetalle>().HasData(detalle1);

            //Factura y su detalle
            var factura2 = new Factura()
            {
                Id = 5,
                FechaCreacion = new DateTime(2022, 02, 03)
            };
            var detalle2 = new List<FacturaDetalle>()
            {
                new FacturaDetalle()
                {
                    Id=9,FacturaId=factura2.Id, Precio=40,Producto="Combo extraordinario"
                },
                new FacturaDetalle()
                {
                    Id=10,FacturaId=factura2.Id, Precio=30,Producto="Combo fiestamix"
                },
                new FacturaDetalle()
                {
                    Id=11,FacturaId=factura2.Id, Precio=65,Producto="Combo picante"
                }
            };
            modelBuilder.Entity<Factura>().HasData(factura2);
            modelBuilder.Entity<FacturaDetalle>().HasData(detalle2);

            //Factura y su detalle
            var factura3 = new Factura()
            {
                Id = 6,
                FechaCreacion = new DateTime(2022, 01, 05)
            };
            var detalle3 = new List<FacturaDetalle>()
            {
                new FacturaDetalle()
                {
                    Id=12,FacturaId=factura3.Id, Precio=25,Producto="Combo amigos"
                },
                new FacturaDetalle()
                {
                    Id=13,FacturaId=factura3.Id, Precio=60,Producto="Combo picante"
                },
                new FacturaDetalle()
                {
                    Id=14,FacturaId=factura3.Id, Precio=75,Producto="Combo dulces"
                }
            };
            modelBuilder.Entity<Factura>().HasData(factura3);
            modelBuilder.Entity<FacturaDetalle>().HasData(detalle3);

        }
    }
}
