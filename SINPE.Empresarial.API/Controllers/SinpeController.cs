using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SINPE.Empresarial.Infrastructure.Data;
using SINPE.Empresarial.Domain.SinpeDomain.Entities;

namespace SINPE.Empresarial.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize] // Requiere autenticación JWT para todos los endpoints
    public class SinpeController : ControllerBase
    {
        private readonly SINPE_Empresarial_DB _db;

        public SinpeController(SINPE_Empresarial_DB db)
        {
            _db = db;
        }

        private IActionResult Res(bool ok, string msg) =>
            Ok(new Dictionary<string, object> { ["EsValido"] = ok, ["Mensaje"] = msg });


        [HttpGet("consultar/{telefonoCaja}")]
        public async Task<IActionResult> Consultar(string telefonoCaja)
        {
            if (string.IsNullOrWhiteSpace(telefonoCaja))
                return BadRequest(new { mensaje = "Debe enviar el número telefónico de la caja." });

            var caja = await _db.Cajas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.TelefonoSINPE == telefonoCaja);

            if (caja == null)
                return NotFound(new { mensaje = "Caja no encontrada." });

            // Validar configuración del comercio (2=Externa, 3=Ambas)
            var config = await _db.Configuraciones
                .AsNoTracking()
                .FirstOrDefaultAsync(cfg => cfg.IdComercio == caja.IdComercio);

            if (config == null)
                return StatusCode(403, new { mensaje = "El comercio no tiene configuración registrada." });

            if (config.TipoConfiguracion != 2 && config.TipoConfiguracion != 3)
                return StatusCode(403, new { mensaje = "El comercio no está autorizado para sincronización externa." });

            var sinpes = await _db.Sinpe
                .AsNoTracking()
                .Where(s => s.TelefonoDestinatario == telefonoCaja)
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

        [HttpPost("sincronizar/{idSinpe:int}")]
        public async Task<IActionResult> Sincronizar(int idSinpe)
        {
            if (idSinpe <= 0)
                return Res(false, "IdSinpe inválido.");

            try
            {
                var pago = await _db.Sinpe.FindAsync(idSinpe);
                if (pago == null)
                    return Res(false, "No existe un SINPE con ese Id.");

                if (pago.Estado) // ya sincronizado
                    return Res(false, "El SINPE ya está sincronizado. No se aplicaron cambios.");

                pago.Estado = true; // 1 = sincronizado
                await _db.SaveChangesAsync();

                return Res(true, "SINPE sincronizado correctamente.");
            }
            catch
            {
                return Res(false, "Ocurrió un error al sincronizar. Intente nuevamente.");
            }
        }

        [HttpPost("recibir")]
        public async Task<IActionResult> Recibir([FromBody] Sinpe body)
        {
            if (body == null)
                return Res(false, "Debe enviar el cuerpo de la solicitud.");

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Dato inválido." : e.ErrorMessage);

                return Res(false, string.Join(" | ", errores));
            }

            if (body.Monto <= 0)
                return Res(false, "El monto debe ser mayor que cero.");

            try
            {
                var caja = await _db.Cajas
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.TelefonoSINPE == body.TelefonoDestinatario);

                if (caja == null)
                    return Res(false, "Caja no encontrada para el teléfono destinatario.");

                if (!caja.Estado)
                    return Res(false, "La caja se encuentra inactiva.");

                var nuevo = new Sinpe
                {
                    TelefonoOrigen = body.TelefonoOrigen,
                    NombreOrigen = body.NombreOrigen,
                    TelefonoDestinatario = body.TelefonoDestinatario,
                    NombreDestinatario = body.NombreDestinatario,
                    Monto = body.Monto,
                    Descripcion = body.Descripcion,
                    FechaDeRegistro = DateTime.Now,
                    Estado = false
                };

                _db.Sinpe.Add(nuevo);
                await _db.SaveChangesAsync();

                return Res(true, "SINPE registrado correctamente.");
            }
            catch
            {
                return Res(false, "Ocurrió un error al registrar el SINPE. Intente nuevamente.");
            }
        }
    }
}
