using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class FacturaDetalleConfig : IEntityTypeConfiguration<FacturaDetalle>
    {
        public void Configure(EntityTypeBuilder<FacturaDetalle> builder)
        {
            builder.Property(fd => fd.Total)
                   .HasComputedColumnSql("Precio * Cantidad");
        }
    }
}
