using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.Property(prop => prop.Titulo).HasMaxLength(250).IsRequired();
            builder.Property(prop => prop.FechaEstreno).HasColumnType("date");
            builder.Property(prop => prop.PosterURL).HasMaxLength(500)
                .IsUnicode(false);//No acepta unicode (emojis por ejemplo)

            builder.HasMany(p => p.Generos) //Una pelicula tiene muchos generos
                   .WithMany(g => g.Peliculas) //Y un genero tiene muchas peliculas
                   .UsingEntity(e=>e.ToTable("GenerosPeliculas")); //Definiendo manualmente el nombre de la tabla en el esquema
        }
    }
}
