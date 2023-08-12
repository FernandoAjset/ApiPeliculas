using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Peliculas.DTOs;
using Peliculas.Entidades;

namespace Peliculas.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

        public GenerosController(ApplicationDbContext context, IMapper mapper, IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<Genero>> Get()
        {
            using (var nuevoContext = dbContextFactory.CreateDbContext())
            {
                nuevoContext.Logs.Add(new Log { Mensaje = "Ejecutando Get en GenerosController" });
                await nuevoContext.SaveChangesAsync();
                return await nuevoContext.Generos
                            .OrderBy(g => g.Nombre)
                            .AsNoTracking().ToListAsync();//el "AsNoTracking()" nos indica que será solo lectura, esto nos ayuda
                                                          //para hacer mas eficiente la aplicación

                //return await context.Generos.AsTracking().ToListAsync();//Sobreescribir la configuración para usar el "AsTracking"
            }
        }

        [HttpGet("procedimiento_almacenado/{id:int}")]
        public async Task<ActionResult<Genero>> GetSP(int id)
        {
            var generos = context.Generos.FromSqlInterpolated($"EXEC Generos_ObtenerPorId {id}")
                                .IgnoreQueryFilters()
                                .AsAsyncEnumerable(); //Solo podemos usar esto, no ToList

            await foreach (var genero in generos)
            {
                return genero;
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {

            //var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);

            //Usando un Query arbitrario con EF Core
            //var genero = await context.Generos
            //                          .FromSqlRaw("SELECT * FROM Generos WHERE identificador={0}", id)
            //                          .IgnoreQueryFilters()
            //                          .FirstOrDefaultAsync();

            //Usando Query arbitrario e interpolación (PARA USAR Query arbitrario debo traer todas las columnas con *)
            var genero = await context.Generos
                                      .FromSqlInterpolated($"SELECT * FROM Generos WHERE identificador={id}")
                                      .IgnoreQueryFilters()
                                      .FirstOrDefaultAsync();

            if (genero is null)
            {
                return NotFound();
            }

            var fechaCreacion = context.Entry(genero).Property<DateTime>("FechaCreacion").CurrentValue;
            return Ok(
                new
                {
                    Id = genero.Identificador,
                    Nombre = genero.Nombre,
                    fechaCreacion
                }
                );
        }

        [HttpGet("primer")]
        public async Task<ActionResult<Genero>> Primer()
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Nombre.StartsWith("C"));
            if (genero is null)
            {
                return NotFound();
            }
            return genero;
        }
        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> Filtrar(string nombre)
        {
            return await context.Generos
                .Where(g => g.Nombre.Contains(nombre))
                .OrderByDescending(g => g.Nombre)
                .ToListAsync();
        }
        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetPaginación(int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 2;
            var generos = await context.Generos
                              .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                              .Take(cantidadRegistrosPorPagina)
                              .ToListAsync();
            return generos;
        }
        [HttpPost("agregarUno")]
        public async Task<ActionResult> Post(Genero genero)
        {
            var existeGeneroConNombre = await context.Generos.AnyAsync(g => g.Nombre == genero.Nombre);
            if (existeGeneroConNombre)
            {
                return BadRequest($"Ya existe un genero con nombre {genero.Nombre}");
            }

            //context.Add(genero);
            //context.Entry(genero).State = EntityState.Added; //Actualizar la entidad directamente
            await context.Database.ExecuteSqlInterpolatedAsync(
                                  $@"INSERT INTO Generos(Nombre)
                                  VALUES({genero.Nombre})");

            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Procedimiento_almacenado")]
        public async Task<ActionResult> PostSP(Genero genero)
        {
            var existeGeneroConNombre = await context.Generos.AnyAsync(g => g.Nombre == genero.Nombre);
            if (existeGeneroConNombre)
            {
                return BadRequest($"Ya existe un genero con nombre {genero.Nombre}");
            }

            //Para poder almacenar el parametro de retorno, en nuestro caso el ID nuevo del registro recien creado
            var outputId = new SqlParameter();
            outputId.ParameterName = "@id";
            outputId.SqlDbType = System.Data.SqlDbType.Int;
            outputId.Direction = System.Data.ParameterDirection.Output;

            //Ejecutar el procedimiento almacenado que permite crear un nuevo registro
            await context.Database.ExecuteSqlRawAsync("EXEC Generos_Insertar @nombre={0}, @id={1} OUTPUT", genero.Nombre, outputId);

            //Casteo del output para tener su valor en una variable tipo entero
            var id = (int)outputId.Value;
            return Ok(id);
        }

        [HttpPost("agregarVarios")]
        public async Task<ActionResult> Post(Genero[] generos)
        {
            context.AddRange(generos);
            await context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("agregarCorrelativo")]
        public async Task<IActionResult> AgregarCorrelativo(int id)
        {
            var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);
            if (genero is null)
            {
                return NotFound();
            }
            genero.Nombre += "2";
            await context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Identificador == id);
            if (genero is null)
            {
                return NotFound();
            }
            context.Remove(genero);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("borradoLogico")]
        public async Task<ActionResult> DeleteLogico(int id)
        {
            var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);
            if (genero is null)
            {
                return NotFound();
            }
            genero.Desactivado = true;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Restaurar")]
        public async Task<ActionResult> Restaurar(int id)
        {
            var genero = await context.Generos.AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(g => g.Identificador == id);
            if (genero is null)
            {
                return NotFound();
            }
            genero.Desactivado = false;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Restaurar_Borrado/{id:int}")]
        public async Task<ActionResult> RestaurarBorrado(int id, DateTime fecha)
        {
            var genero = await context.Generos.TemporalAsOf(fecha).AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(g => g.Identificador == id);
            if (genero is null)
            {
                return NotFound();
            }
            try
            {
                await context.Database.ExecuteSqlInterpolatedAsync($@"
                SET IDENTITY_INSERT Generos ON;
                INSERT INTO Generos (Identificador, Nombre)
                VALUES({genero.Identificador}, {genero.Nombre})
                SET IDENTITY_INSERT Generos OFF;
                ");
            }
            finally
            {
                await context.Database.ExecuteSqlRawAsync($@"
                SET IDENTITY_INSERT Generos OFF;
                ");
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(GeneroActualizacionDTO generoActualizacionDTO)
        {
            var genero = mapper.Map<Genero>(generoActualizacionDTO);
            context.Update(genero);

            context.Entry(genero).Property(g => g.Nombre).OriginalValue = generoActualizacionDTO.Nombre_Original;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("modificar_varias_veces")]
        public async Task<ActionResult> ModificarVariasVeces()
        {
            var id = 2005;
            var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);

            genero.Nombre = "Anime chino";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            genero.Nombre = "Anime jopenes";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            genero.Nombre = "Anime japones";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            return Ok();
        }

        [HttpGet("Temporal/{id:int}")]
        public async Task<ActionResult<Genero>> GetTemporal(int id)
        {

            var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null)
            {
                return NotFound();
            }

            var fechaCreacion = context.Entry(genero).Property<DateTime>("FechaCreacion").CurrentValue;
            var periodStart = context.Entry(genero).Property<DateTime>("PeriodStart").CurrentValue;
            var periodEnd = context.Entry(genero).Property<DateTime>("PeriodEnd").CurrentValue;
            return Ok(
                new
                {
                    Id = genero.Identificador,
                    Nombre = genero.Nombre,
                    fechaCreacion,
                    periodStart,
                    periodEnd
                }
                );
        }

        [HttpGet("TemporalAll/{id:int}")]
        public async Task<ActionResult> GetTemporalAll(int id)
        {
            var generos = await context.Generos.TemporalAll().AsTracking().Where(g => g.Identificador == id)
                                       .Select(g => new
                                       {
                                           Id = g.Identificador,
                                           Nombre = g.Nombre,
                                           PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                           PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .OrderByDescending(g => g.PeriodStart)
                                       .ToListAsync();

            return Ok(generos);
        }

        [HttpGet("TemporalAsOf/{id:int}")]
        public async Task<ActionResult> GetTemporalAsOf(int id, DateTime fecha)
        {
            var generos = await context.Generos.TemporalAsOf(fecha).AsTracking()
                                .Where(g => g.Identificador == id)
                                .Select(g => new
                                {
                                    Id = g.Identificador,
                                    Nombre = g.Nombre,
                                    PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                    PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd"),
                                }).FirstOrDefaultAsync();

            return Ok(generos);
        }

        [HttpGet("TemporalFromTo/{id:int}")]
        public async Task<ActionResult> GetTemporalFromTo(int id, DateTime desde, DateTime hasta)
        {
            var generos = await context.Generos.TemporalFromTo(desde,hasta).AsTracking().Where(g => g.Identificador == id)
                                       .Select(g => new
                                       {
                                           Id = g.Identificador,
                                           Nombre = g.Nombre,
                                           PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                           PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .OrderByDescending(g => g.PeriodStart)
                                       .ToListAsync();
            return Ok(generos);
        }

        [HttpGet("TemporalContainedIn/{id:int}")]
        public async Task<ActionResult> GetTemporalContainedIn(int id, DateTime desde, DateTime hasta)
        {
            var generos = await context.Generos.TemporalContainedIn(desde, hasta).AsTracking().Where(g => g.Identificador == id)
                                       .Select(g => new
                                       {
                                           Id = g.Identificador,
                                           Nombre = g.Nombre,
                                           PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                           PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .OrderByDescending(g => g.PeriodStart)
                                       .ToListAsync();
            return Ok(generos);
        }

        [HttpGet("TemporalBetween/{id:int}")]
        public async Task<ActionResult> GetTemporalBetween(int id, DateTime desde, DateTime hasta)
        {
            var generos = await context.Generos.TemporalBetween(desde, hasta).AsTracking().Where(g => g.Identificador == id)
                                       .Select(g => new
                                       {
                                           Id = g.Identificador,
                                           Nombre = g.Nombre,
                                           PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                           PeriodEnd = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .OrderByDescending(g => g.PeriodStart)
                                       .ToListAsync();
            return Ok(generos);
        }
    }
}
