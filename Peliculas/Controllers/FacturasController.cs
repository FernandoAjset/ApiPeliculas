using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peliculas.Entidades;
using Peliculas.Entidades.Funciones;

namespace Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<FacturasController> logger;

        public FacturasController(ApplicationDbContext context, ILogger<FacturasController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        [HttpGet("Funciones_escalares")]
        public async Task<ActionResult> GetFuncionesEscalares()
        {
            var facturas = await context.Facturas.Select(f => new
            {
                Id = f.Id,
                Total = context.FacturaDetalleSuma(f.Id),
                Promedio = Escalares.FacturaDetallePromedio(f.Id),
            })
                .OrderBy(f => context.FacturaDetalleSuma(f.Id))
                .ToListAsync();

            return Ok(facturas);
        }

        [HttpGet("{id:int}/detalle")]
        public async Task<ActionResult<IEnumerable<FacturaDetalle>>> GetDetalle(int id)
        {
            return await context.FacturaDetalles.Where(f => f.FacturaId == id)
                                .OrderByDescending(f => f.Total)
                                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                var factura = new Factura()
                {
                    FechaCreacion = DateTime.Now
                };

                context.Add(factura);
                await context.SaveChangesAsync();

                var facturaDetalle = new List<FacturaDetalle>()
                {
                    new FacturaDetalle()
                    {
                        Producto ="Combo Individual",
                        Precio=40,
                        FacturaId=factura.Id
                    },
                    new FacturaDetalle()
                    {
                        Producto="Combo Amigos",
                        Precio=70,
                        FacturaId=factura.Id
                    }
                };

                context.AddRange(facturaDetalle);
                await context.SaveChangesAsync();
                await transaccion.CommitAsync();
                return Ok("Registro creado");
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                return BadRequest("Hubo un error" + ex);
            }
        }

        [HttpPost("Concurrencia_Fila_Manejo_Error")]
        public async Task<ActionResult> ConcurrenciaFilaManejandoError()
        {
            var facturaId = 3;
            try
            {
                var factura = await context.Facturas.AsTracking().FirstOrDefaultAsync(f => f.Id == facturaId);
                //Actualización por usuario1
                factura.FechaCreacion = DateTime.Now.AddDays(-10);

                //Actualización por usuario2
                await context.Database.ExecuteSqlInterpolatedAsync(@$"UPDATE Facturas SET FechaCreacion=GetDate()
                                                                WHERE ID={facturaId}");

                //Confirma cambios usuario1
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single(); //Single es porque solo es un registro, si fueran varios es suficiente con Entries
                var facturaActual = await context.Facturas.AsNoTracking().FirstOrDefaultAsync(f => f.Id == facturaId);

                foreach (var propiedad in entry.Metadata.GetProperties())
                {
                    var valorIntentado = entry.Property(propiedad.Name).CurrentValue;
                    var valorDbActual = context.Entry(facturaActual).Property(propiedad.Name).CurrentValue;
                    var valorAnterior = entry.Property(propiedad.Name).OriginalValue;

                    if (valorDbActual.ToString() == valorIntentado.ToString())
                    {
                        //Esta propiedad no fue modificada
                        continue;
                    }

                    logger.LogInformation($"--- Propiedad {propiedad.Name} ---");
                    logger.LogInformation($"Valor intentado: {valorIntentado}");
                    logger.LogInformation($"Valor en la base de datos: {valorDbActual}");
                    logger.LogInformation($"Valor anterior: {valorAnterior}");

                    // podemos seguir haciendo operaciones especifícas....
                }
                return BadRequest("Registro no puedo ser actualizado, porque fue modificado por otra persona");
            }
        }
        [HttpGet("ObtenerFactura")]
        public async Task<ActionResult<Factura>> ObtenerFactura(int id)
        {
            var factura = await context.Facturas.FirstOrDefaultAsync(f => f.Id == id);
            if (factura is null)
            {
                return NotFound();
            }
            return factura;
        }

        [HttpPut("ActualizarFactura")]
        public async Task<ActionResult> ActualizarFactura(Factura factura)
        {
            context.Update(factura);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
