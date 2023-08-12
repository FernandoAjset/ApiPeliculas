using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Peliculas.Entidades.Configuraciones
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            //Configurar entidad, como tabla temporal
            builder.ToTable(name: "Generos", opciones => opciones.IsTemporal());
            //Convirtiendo el tipo de dato de datetime a datetime2
            builder.Property<DateTime>("PeriodStart").HasColumnType("datetime2");
            builder.Property<DateTime>("PeriodEnd").HasColumnType("datetime2");

            //API Fluente para configurar un atributo de una entidad
            builder.HasKey(prop => prop.Identificador); //Indicar campo como llave primaria, cuando el campo no se llama Id
            //Modificar el nombre de la columna, poner maximo de largo y no null
            //modelBuilder.Entity<Genero>().Property(prop => prop.Nombre).HasColumnName("NombreGenero").HasMaxLength(150).IsRequired();
            builder.Property(prop => prop.Nombre).HasMaxLength(150).IsRequired();
            //Modificar el nombre de la tabla y el esquema
            //modelBuilder.Entity<Genero>().ToTable(name: "TablaGeneros", schema: "Peliculas");
            builder.HasQueryFilter(g => !g.Desactivado); //filtro para traer solo los registros donde el desactivado no sea verdadero.
            builder.HasIndex(g => g.Nombre).IsUnique().HasFilter("Desactivado='false'"); //Indice sobre el campo Nombre, para que su valor sea unico
                                                                                         //Además se esta aplicando filtro al indice
            builder.Property<DateTime>("FechaCreacion").HasDefaultValueSql("GetDate()").HasColumnType("datetime2");
        }
    }
}
