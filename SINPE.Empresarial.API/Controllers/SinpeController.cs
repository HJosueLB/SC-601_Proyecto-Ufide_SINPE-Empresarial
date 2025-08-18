using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;                          
using Microsoft.AspNetCore.Mvc;
using SINPE.Empresarial.Infrastructure.Data;       

namespace SINPE.Empresarial.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinpeController : ControllerBase
    {
        private readonly SINPE_Empresarial_DB _db;

        public SinpeController(SINPE_Empresarial_DB db)
        {
            _db = db;
        }

        // GET: /api/sinpe/consultar/{telefonoCaja}
        [HttpGet("consultar/{telefonoCaja}")]
        public async Task<IActionResult> Consultar(string telefonoCaja)
        {
            if (string.IsNullOrWhiteSpace(telefonoCaja))
                return BadRequest(new { mensaje = "Debe enviar el número telefónico de la caja." });

            // 1) Buscar la caja (por TelefonoSINPE)
            var caja = await _db.Cajas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.TelefonoSINPE == telefonoCaja);

            if (caja == null)
                return NotFound(new { mensaje = "Caja no encontrada." });

            // 2) Validar configuración del comercio en la tabla Configuraciones
            //    Regla: TipoConfiguracion == 2 (Externa) o 3 (Ambas)
            var config = await _db.Configuraciones
                .AsNoTracking()
                .FirstOrDefaultAsync(cfg => cfg.IdComercio == caja.IdComercio);

            if (config == null)
                return StatusCode(403, new { mensaje = "El comercio no tiene configuración registrada." });

            if (config.TipoConfiguracion != 2 && config.TipoConfiguracion != 3)
                return StatusCode(403, new { mensaje = "El comercio no está autorizado para sincronización externa." });

            // 3) Traer SINPE asociados a la caja (por teléfono destinatario)
            var sinpes = await _db.Sinpe
                .AsNoTracking()
                .Where(s => s.TelefonoDestinatario == telefonoCaja)  // usa CajaId == caja.Id si así es tu modelo
                .OrderByDescending(s => s.FechaDeRegistro)
                .Select(s => new
                {
                    IdSinpe = s.IdSinpe,
                    TelefonoOrigen = s.TelefonoOrigen,
                    NombreOrigen = s.NombreOrigen,
                    TelefonoDestinatario = s.TelefonoDestinatario,
                    NombreDestinatario = s.NombreDestinatario,
                    Monto = s.Monto,
                    Descripcion = s.Descripcion,
                    Fecha = s.FechaDeRegistro,
                    Estado = s.Estado
                })
                .ToListAsync();

            return Ok(sinpes);
        }
    }
}
