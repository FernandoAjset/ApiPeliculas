using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class CineConfig : IEntityTypeConfiguration<Cine>
    {
        public void Configure(EntityTypeBuilder<Cine> builder)
        {
            //builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications); // SOLO USAR EN Estrategia de Detección de cambios personalizada

            builder.Property(prop => prop.Nombre).HasMaxLength(150).IsRequired();//No acepta nulos

            //CONFIGURACION EN API FLUENTE PARA RELACIONES UNO A UNO
            builder.HasOne(c => c.CineOferta)
                   .WithOne()                                  //Relacion uno a uno por medio del API Fluente
                   .HasForeignKey<CineOferta>(co => co.CineId); //Llave foranea desde el API FLuente

            //CONFIGURACION EN API FLUENTE PARA RELACIONES UNO A MUCHOS
            builder.HasMany(c => c.SalasDeCine) //A un cine le corresponden muchas salas de cine
                .WithOne(s => s.Cine)           //Mientras que a una sala de cine, le corresponde un cine
                .HasForeignKey(s => s.CineIdentificador) //Llave foranea en sala de cine
                .OnDelete(DeleteBehavior.Cascade); //Configurando Restrict para que marque excepción si se trata borrar la entidad principal, de lo contrario
                                                   //Cascade va a borrar la entidad y sus referencias


            builder.HasOne(c => c.CineDetalle)
                   .WithOne(cd => cd.Cine)
                   .HasForeignKey<CineDetalle>(cd => cd.Id); //La misma llave foranea va a coincidir con la primaria de Cine

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
