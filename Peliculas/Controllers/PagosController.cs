using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peliculas.Entidades;

namespace Peliculas.Controllers
{
    [ApiController]
    [Route("api/pagos")]
    public class PagosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PagosController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> Get()
        {
            return await context.Pagos.ToListAsync();
        }

        [HttpGet("tarjetas")]
        public async Task<ActionResult<IEnumerable<PagoTarjeta>>> GetPagoTarjeta()
        {
            return await context.Pagos.OfType<PagoTarjeta>().ToListAsync();
        }
        [HttpGet("paypal")]
        public async Task<ActionResult<IEnumerable<PagoPaypal>>> GetPagoPaypal()
        {
            return await context.Pagos.OfType<PagoPaypal>().ToListAsync();
        }
    }
}
