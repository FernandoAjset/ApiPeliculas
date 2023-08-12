using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(prop => prop.Nombre).HasMaxLength(150).IsRequired();//Tamaño maximo para propiedad Nombre
            builder.Property(prop => prop.FechaNacimiento).HasColumnType("date");//Cambiar el tipo de dato a otro
            builder.Property(prop => prop.Nombre).HasField("_nombre");
            builder.Ignore(a => a.Edad);


            //Cambiar las propiedades de dirección para no seguir la convención
            builder.OwnsOne(c => c.Direccion, dir =>
            {
                dir.Property(d => d.Calle).HasColumnName("Calle");
                dir.Property(d => d.Provincia).HasColumnName("Provincia");
                dir.Property(d => d.Pais).HasColumnName("Pais");
            });

        }
    }
}
