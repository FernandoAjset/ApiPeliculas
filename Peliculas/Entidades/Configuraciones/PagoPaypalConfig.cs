using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class PagoPaypalConfig : IEntityTypeConfiguration<PagoPaypal>
    {
        public void Configure(EntityTypeBuilder<PagoPaypal> builder)
        {
            builder.Property(p => p.CorreoElectronico).HasMaxLength(150).IsRequired();

            var pago1 = new PagoPaypal()
            {
                Id = 3,
                FechaTransaccion = new DateTime(2022, 1, 7),
                Monto = 150,
                TipoPago = TipoPago.Paypal,
                CorreoElectronico = "hgarcia@mail.com"
            };

            var pago2 = new PagoPaypal()
            {
                Id = 5,
                FechaTransaccion = new DateTime(2022, 1, 8),
                Monto = 120,
                TipoPago = TipoPago.Paypal,
                CorreoElectronico = "kmlopez@mail.com"
            };

            builder.HasData(pago1, pago2);
        }
    }
}
