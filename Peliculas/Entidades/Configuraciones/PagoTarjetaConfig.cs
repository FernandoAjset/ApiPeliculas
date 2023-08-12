using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class PagoTarjetaConfig : IEntityTypeConfiguration<PagoTarjeta>
    {
        public void Configure(EntityTypeBuilder<PagoTarjeta> builder)
        {
            builder.Property(p => p.UltimosDigitos).HasColumnType("char(4)").IsRequired();

            var pago1 = new PagoTarjeta()
            {
                Id = 8,
                FechaTransaccion = new DateTime(2022, 2, 7),
                Monto = 150,
                TipoPago = TipoPago.Tarjeta,
                UltimosDigitos = "0123"
            };

            var pago2 = new PagoTarjeta()
            {
                Id = 15,
                FechaTransaccion = new DateTime(2022, 2, 8),
                Monto = 120,
                TipoPago = TipoPago.Tarjeta,
                UltimosDigitos = "2468"
            };

            builder.HasData(pago1, pago2);
        }
    }
}
