using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class CineDetalleConfig : IEntityTypeConfiguration<CineDetalle>
    {
        public void Configure(EntityTypeBuilder<CineDetalle> builder)
        {
            builder.ToTable("Cines");
        }
    }
}
