using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Peliculas.Entidades.Conversiones;

namespace Peliculas.Entidades.Configuraciones
{
    public class SalaDeCineConfig : IEntityTypeConfiguration<SalaDeCine>
    {
        public void Configure(EntityTypeBuilder<SalaDeCine> builder)
        {
            builder.Property(prop => prop.Precio).HasPrecision(6, 2);//Presicion para un campo con decimales
            builder.Property(prop => prop.TipoSalaDeCine)
                .HasDefaultValue(TipoSalaDeCine.DosDimensiones)//Agregando un valor por defecto
                .HasConversion<string>();//transformando el valor numerico de la ENUM a cadena
            builder.Property(prop => prop.Moneda)
                .HasConversion<MonedaASimboloConverter>();
        }
    }
}
