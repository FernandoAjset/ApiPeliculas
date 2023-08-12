using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peliculas.Entidades;

namespace Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ProductoController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            return await context.Productos.ToListAsync();
        }
        [HttpGet("merchs")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetMerchs()
        {
            return await context.Set<Merchandising>().ToListAsync();
        }
        [HttpGet("peliculasAlquilables")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetPeliculasAlquilables()
        {
            return await context.Set<PeliculaAlquilable>().ToListAsync();
        }
    }
}
