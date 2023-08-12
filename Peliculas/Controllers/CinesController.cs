using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Peliculas.DTOs;
using Peliculas.Entidades;
using Peliculas.Entidades.SinLlave;
using System.Collections.ObjectModel;

namespace Peliculas.Controllers
{
    [Route("api/cines")]
    [ApiController]
    public class CinesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<CineDTO>> Get()
        {
            return await context.Cines.ProjectTo<CineDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetConId(int id)
        {
            //var cineDB=await context.Cines.AsTracking()
            //    .Include(c=>c.SalasDeCine)
            //    .Include(c=>c.CineOferta)
            //    .Include(c=>c.CineDetalle)
            //    .FirstOrDefaultAsync(c=>c.Id==id);

            //Combinando Query arbitrario con funciones LinQ
            var cineDB = await context.Cines
                                    .FromSqlInterpolated($"SELECT * FROM cines WHERE Id={id}")
                                    .Include(c => c.SalasDeCine)
                                    .FirstOrDefaultAsync();
            if (cineDB is null)
            {
                return NotFound();
            }
            cineDB.Ubicacion = null;
            return Ok(cineDB);
        }
        [HttpGet("cercanos")]
        public async Task<ActionResult> Get(double latitud, double longitud)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var miUbicacion = geometryFactory.CreatePoint(new Coordinate(longitud, latitud));
            var distanciaMaximaEnMetros = 2000;
            var cines = await context.Cines
                .OrderBy(c => c.Ubicacion.Distance(miUbicacion))
                .Where(c => c.Ubicacion.IsWithinDistance(miUbicacion, distanciaMaximaEnMetros))
                .Select(c => new
                {
                    Nombre = c.Nombre,
                    Distancia = Math.Round(c.Ubicacion.Distance(miUbicacion))
                })
                .ToListAsync();

            return Ok(cines);
        }
        [HttpGet("SinUbicacion")]
        public async Task<IEnumerable<CineSinUbicacion>> GetCinesSinUbicacion()
        {
            //return await context.Set<CineSinUbicacion>().ToListAsync();   Esta linea es en caso de no tener el DbSet en el context

            return await context.CinesSinUbicaciones.ToListAsync();
        }
        [HttpPost("postDataRelacionada")]
        public async Task<ActionResult> PostDataRelacionada()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var ubicacionCine = geometryFactory.CreatePoint(new Coordinate(-90.989213, 14.328016));

            var cine = new Cine()
            {
                Nombre = "Mi Cine, cine con detalle",
                Ubicacion = ubicacionCine,
                CineOferta = new CineOferta()
                {
                    PorcentajeDescuento = 5,
                    FechaInicio = DateTime.Today,
                    FechaFin = DateTime.Today.AddDays(7)
                },
                SalasDeCine = new ObservableCollection<SalaDeCine>()
                {
                    new SalaDeCine()
                    {
                        Precio = 7,
                        Moneda=Moneda.DolarEstadounidense,
                        TipoSalaDeCine = TipoSalaDeCine.DosDimensiones
                    },
                    new SalaDeCine()
                    {
                        Precio = 70,
                        Moneda=Moneda.Quetzal,
                        TipoSalaDeCine = TipoSalaDeCine.TresDimensiones
                    }
                },
                CineDetalle = new CineDetalle()
                {
                    Historia = "Desde hace mas de 75 años en Guatemala",
                    Mision = "Ser el numero 1 en entretenimiento",
                    Valores = "Respeto, Honestidad, Libertad"
                },
                Direccion = new Direccion()
                {
                    Calle = "Km 88",
                    Provincia = "Escuintla",
                    Pais = "Guatemala"
                }
            };

            context.Add(cine);
            context.SaveChanges();
            return Ok();
        }

        [HttpPost("conDTO")]
        public async Task<ActionResult> Post(CineCreacionDTO cineCreacionDTO)
        {
            var cine = mapper.Map<Cine>(cineCreacionDTO);
            context.Add(cine);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cine = await context.Cines
                                    .Include(c => c.CineOferta)
                                    .Include(c => c.SalasDeCine)
                                    .FirstOrDefaultAsync(c => c.Id == id);
            if (cine is null)
            {
                return NotFound();
            }
            context.RemoveRange(cine.SalasDeCine); //Borrando varias salas de cine que dependen de Cine, antes de borrar Cine
            context.Remove(cine);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
