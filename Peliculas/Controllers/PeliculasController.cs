using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peliculas.DTOs;
using Peliculas.Entidades;
using Peliculas.Entidades.SinLlave;

namespace Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("PeliculasConConteos/{id:int}")]
        public async Task<ActionResult<PeliculasConConteos>> GetPeliculasConConteos(int id)
        {
            var resultado = await context.PeliculaConConteos(id).FirstOrDefaultAsync();
            if(resultado is null)
            {
                return NotFound();
            }
            return resultado;
            //return await context.Set<PeliculasConConteos>().ToListAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas
                .Include(p => p.Generos.OrderBy(g => g.Nombre))
                .Include(p => p.SalasDeCine)
                    .ThenInclude(s => s.Cine)
                .Include(p => p.PeliculasActores.Where(pa => pa.Actor.FechaNacimiento.Value.Year >= 1980))
                    .ThenInclude(pa => pa.Actor)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pelicula is null)
            {
                return NotFound();
            }

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            peliculaDTO.Cines = peliculaDTO.Cines.DistinctBy(x => x.Id).ToList();
            return peliculaDTO;
        }
        [HttpGet("conprojectto/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetProjectTo(int id)
        {
            var pelicula = await context.Peliculas
                .ProjectTo<PeliculaDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pelicula is null)
            {
                return NotFound();
            }

            pelicula.Cines = pelicula.Cines.DistinctBy(x => x.Id).ToList();
            return pelicula;
        }
        [HttpGet("cargadoselectivo/{id:int}")]
        public async Task<IActionResult> GetSelectivo(int id)
        {
            var pelicula = await context.Peliculas
                                .Select(p => new
                                {
                                    Id = p.Id,
                                    Titulo = p.Titulo,
                                    Generos = p.Generos
                                               .OrderBy(p => p.Nombre)
                                               .Select(g => g.Nombre),
                                    CantidadActores = p.PeliculasActores.Count(),
                                    CantidadCines = p.SalasDeCine.Select(s => s.CineIdentificador).Distinct().Count(),
                                })
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }
        [HttpGet("cargadoexplicito/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetExplicito(int id)
        {
            var pelicula = await context.Peliculas
                               .AsTracking()
                               .FirstOrDefaultAsync(p => p.Id == id);
            //...
            //await context.Entry(pelicula).Collection(p => p.Generos).LoadAsync();

            var cantidadGeneros = await context.Entry(pelicula).Collection(p => p.Generos).Query().CountAsync();
            if (pelicula is null)
            {
                return NotFound();
            }

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);

            return peliculaDTO;
        }
        [HttpGet("agrupadasPorEstreno")]
        public async Task<ActionResult> GetAgrupadasPorCartelera()
        {
            var peliculasAgrupadas = await context.Peliculas.GroupBy(p => p.EnCartelera)
                                         .Select(g => new
                                         {
                                             EnCartelera = g.Key,
                                             Conteo = g.Count(),
                                             Peliculas = g.ToList()
                                         }).ToListAsync();

            return Ok(peliculasAgrupadas);
        }
        [HttpGet("agruparPorCantidadDeGeneros")]
        public async Task<IActionResult> GetAgrupadasPorCantidadDeGeneros()
        {
            var peliculasAgrupadas = await context.Peliculas.GroupBy(p => p.Generos.Count())
                                   .Select(g => new
                                   {
                                       Conteo = g.Key,
                                       Generos = g.Select(p => p.Generos.Select(g => g.Nombre)).SelectMany(gen => gen).Distinct(),
                                       Titulos = g.Select(p => p.Titulo)
                                   }
                                   ).ToListAsync();

            return Ok(peliculasAgrupadas);
        }
        [HttpGet("filtrar")]
        public async Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] PeliculasFiltroDTO peliculasFiltroDTO)
        {
            var peliculasQueryable = context.Peliculas.AsQueryable();
            if (!string.IsNullOrEmpty(peliculasFiltroDTO.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.Titulo.Contains(peliculasFiltroDTO.Titulo));
            }

            if (peliculasFiltroDTO.EnCartelera)
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.EnCartelera);
            }
            if (peliculasFiltroDTO.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculasQueryable = peliculasQueryable.Where(p => p.FechaEstreno > hoy);
            }
            if (peliculasFiltroDTO.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.Generos
                                                                    .Select(g => g.Identificador)
                                                                    .Contains(peliculasFiltroDTO.GeneroId));
            }

            var peliculas = await peliculasQueryable.Include(p => p.Generos).ToListAsync();
            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }
        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);
            pelicula.Generos.ForEach(g => context.Entry(g).State = EntityState.Unchanged);
            pelicula.SalasDeCine.ForEach(s => context.Entry(s).State = EntityState.Unchanged);

            if (pelicula.PeliculasActores is not null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
