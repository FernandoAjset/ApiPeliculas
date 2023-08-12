using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class PeliculaActorConfig : IEntityTypeConfiguration<PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<PeliculaActor> builder)
        {
            builder.HasKey(prop => new { prop.PeliculaId, prop.ActorId });//Creación de llave compuesta
            builder.Property(prop => prop.Personaje).HasMaxLength(150);

            //CONFIGURACION DE RELACIONES MUCHOS A MUCHOS
            builder.HasOne(pa => pa.Actor)
                   .WithMany(a => a.PeliculasActores)
                   .HasForeignKey(pa => pa.ActorId);

            builder.HasOne(pa => pa.Pelicula)
                   .WithMany(p => p.PeliculasActores)
                   .HasForeignKey(pa => pa.PeliculaId);
        }
    }
}
