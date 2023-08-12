using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class PagosConfig : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            //Discriminador para que reconozca que tipo de instancia va a usars
            builder.HasDiscriminator(p => p.TipoPago)
                   .HasValue<PagoPaypal>(TipoPago.Paypal)
                   .HasValue<PagoTarjeta>(TipoPago.Tarjeta);

            builder.Property(p => p.Monto).HasPrecision(18, 2);
        }
    }
}
