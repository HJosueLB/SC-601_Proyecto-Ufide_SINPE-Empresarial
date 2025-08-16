using Microsoft.AspNetCore.Mvc;
using SINPE_Empresarial.Services;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;

namespace SINPE_Empresarial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajasController : Controller
    {
        private readonly ICajaService _cajaService;

        public CajasController(ICajaService cajaService)
        {
            _cajaService = cajaService;
        }

        [HttpGet("{idComercio}")]
        public IActionResult ObtenerCajasPorComercio(int idComercio)
        {
            var cajas = _cajaService.ObtenerCajasPorComercio(idComercio);
            return Ok(cajas);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerCaja(int id)
        {
            var caja = _cajaService.ObtenerCaja(id);
            if (caja == null)
            {
                return NotFound();
            }
            return Ok(caja);
        }

        [HttpPost]
        public IActionResult CrearCaja([FromBody] Caja caja)
        {
            try
            {
                _cajaService.CrearCaja(caja);
                return CreatedAtAction(nameof(ObtenerCaja), new { id = caja.IdCaja }, caja);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarCaja(int id, [FromBody] Caja caja)
        {
            if (id != caja.IdCaja)
            {
                return BadRequest();
            }

            try
            {
                _cajaService.EditarCaja(caja);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCaja(int id)
        {
            _cajaService.EliminarCaja(id);
            return NoContent();
        }
    }
}