using Microsoft.EntityFrameworkCore;
using Peliculas.Entidades;
using Peliculas.Entidades.Configuraciones;
using Peliculas.Entidades.Funciones;
using Peliculas.Entidades.Seeding;
using Peliculas.Entidades.SinLlave;
using Peliculas.Servicios;
using System.Reflection;

namespace Peliculas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<CineOferta> CinesOfertas { get; set; }
        public DbSet<SalaDeCine> SalasDeCines { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CineSinUbicacion> CinesSinUbicaciones { get; set; }
        //public DbSet<PeliculasConConteos> PeliculasConConteos { get; set; } //Solo agregar si llama a una vista, de lo contrario creará la entidad aunque esté marcada como Ignore
        public DbSet<CineDetalle> CinesDetalles { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        [DbFunction(name: "FacturaDetalleSuma")] //Especificando nombre de la función en la base de datos
        public int FacturaDetalleSuma(int facturaId)
        {
            return 0;
        }
        //Plantilla que representa función definida por el usuario, con mismo nombre que el método
        //Esta función devuelve valores de tabla (filas y columnas)
        [DbFunction]
        public IQueryable<PeliculasConConteos> PeliculaConConteos(int peliculaId)
        {
            return FromExpression(() => PeliculaConConteos(peliculaId));
        }
        //Sobreescritura de método para hacer la configuración de la conexión desde el DbContext
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) //Comprobar si no existe una configuración previa en el Program.cs
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection", opciones =>
                {
                    opciones.UseNetTopologySuite();
                }
                ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");//Al hacer la convención siempre que realice un campo 
                                                                               //tipo DateTime se pasará a date.
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("NumeroFactura", "factura");
            Escalares.RegistrarFunciones(modelBuilder); //Método de la clase Escalares que configura las funciones en una sola clase
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new GeneroConfig()); esto es en el caso de implementar una configuracion para cada entidad
            //modelBuilder.Ignore<ClasePrueba>(); //Ignora una clase completa, para no llevarla a la BD
            modelBuilder.Ignore<PeliculasConConteos>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Escanea todo el proyecto (Assembly) y toma las clases que
                                                                                          //heredan de IEntityTypeConfiguration y ponlas en nuestro 
                                                                                          //API fluente
            modelBuilder.Entity<CineSinUbicacion>()
                        .HasNoKey().ToSqlQuery("SELECT Id, Nombre FROM Cines")
                        .ToView(null);//Configura la entidad sin llave primaria y además el Query por defecto, el ToView(null) evita que se 
                                      //cree una tabla en el esquema

            //modelBuilder.Entity<PeliculasConConteos>().HasNoKey().ToView("PeliculasConConteos");//Agregando una vista

            //Centralizando Queries Arbitrarios en lugar de una vista
            //modelBuilder.Entity<PeliculasConConteos>().HasNoKey().ToSqlQuery(
            //    @"SELECT Id, Titulo,
            //    (SELECT COUNT(*) FROM GenerosPeliculas
            //    WHERE PeliculasId = Peliculas.Id) AS CantidadGeneros,
            //    (SELECT COUNT(DISTINCT Id) FROM PeliculaSalaDeCine
            //    INNER JOIN SalasDeCines
            //    ON SalasDeCines.Id = PeliculaSalaDeCine.SalasDeCineId
            //    WHERE PeliculasId = Peliculas.Id) AS CantidadCines,
            //    (SELECT COUNT(*) FROM PeliculasActores
            //    WHERE PeliculaId = Peliculas.Id) AS CantidadActores
            //    FROM Peliculas"
            //    );

            modelBuilder.Entity<PeliculasConConteos>().HasNoKey().ToTable(name: null);
            modelBuilder.HasDbFunction(() => PeliculaConConteos(0));
            foreach (var tipoEntidad in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var propiedad in tipoEntidad.GetProperties())
                {
                    if (propiedad.ClrType == typeof(string) && propiedad.Name.Contains("URL", StringComparison.CurrentCultureIgnoreCase))
                    {
                        propiedad.SetIsUnicode(false);
                        propiedad.SetMaxLength(500);
                    }
                }
            }
            if (!Database.IsInMemory())
            {
                SeedingModuloConsulta.Seed(modelBuilder);
                SeedingFactura.Seed(modelBuilder);
            }
            modelBuilder.Entity<Merchandising>().ToTable("Merchandising");
            modelBuilder.Entity<PeliculaAlquilable>().ToTable("PeliculasAlquilables");
            var pelicula1 = new PeliculaAlquilable()
            {
                Id = 1,
                Nombre = "Spider-Man",
                PeliculaId = 1,
                Precio = (double)5.99m
            };
            var pelicula2 = new PeliculaAlquilable()
            {
                Id = 4,
                Nombre = "The Matrix Resurrections",
                PeliculaId = 5,
                Precio = (double)5.99m
            };
            var merch1 = new Merchandising()
            {
                Id = 2,
                DisponibleEnInventario = true,
                EsRopa = true,
                Nombre = "T-shirt One Piece",
                Peso = 1,
                Volumen = 1,
                Precio = 15
            };
            modelBuilder.Entity<PeliculaAlquilable>().HasData(pelicula1, pelicula2);
            modelBuilder.Entity<Merchandising>().HasData(merch1);
        }
    }
}
