using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peliculas.DTOs;
using Peliculas.Entidades;

namespace Peliculas.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get()
        {
            //var actores = await context.Actores.Select(
            //    a => new ActorDTO { Id = a.Id, Nombre = a.Nombre }).ToListAsync();
            var actores = await context.Actores
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
            return actores;
        }
        [HttpPost]
        public async Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(ActorCreacionDTO actorDTO, int id)
        {
            var actorDB = await context.Actores.AsTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (actorDB is null)
            {
                return NotFound();
            }

            actorDB = mapper.Map(actorDTO, actorDB);
            var entry = context.Entry(actorDB);
            //await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("desconectado/{id:int}")]
        public async Task<ActionResult> PutDesconectado(ActorCreacionDTO actorCreacionDTO, int id)
        {
            var existeActor = await context.Actores.AnyAsync(a => a.Id == id);
            if (!existeActor)
            {
                return NotFound();
            }

            var actor = mapper.Map<Actor>(actorCreacionDTO);
            actor.Id = id;
            //context.Update(actor);

            //Actualizar una sola propiedad de una entidad, configurando la columna que vamos a modificar
            //Esta tecnica solo sirve para hacer mas veloz el Query, ya que no actualiza todos los datos, solo actualiza un campo.
            context.Entry(actor)
                   .Property(
                    a => a.Nombre
                    ).IsModified = true;

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
