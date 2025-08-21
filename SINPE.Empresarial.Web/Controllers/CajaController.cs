using SINPE.Empresarial.Domain.BitacoraDomain.Entities;
using SINPE.Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE.Empresarial.Domain.CajaDomain.Entities;
using SINPE.Empresarial.Infrastructure.Data;
using SINPE.Empresarial.Infrastructure.Repositories;
using SINPE.Empresarial.Infrastructure.Services;
using SINPE.Empresarial.Web.ApplicationModels.Mappers;

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SINPE.Empresarial.Web.Controllers
{
    [Authorize(Roles = "Administrador,Cajero")]
    public class CajaController : Controller
    {
        // Instancia: Servicio de caja
        private readonly CajaService _cajaService;

        // Instancia: Servicio de Comercio
        private readonly ComercioService _comercioService;

        // Instancia: Servicio de bitacora
        private readonly BitacoraService _bitacoraService;

        // Constructor sin parámetros (por compatibilidad con MVC sin IoC)
        public CajaController()
        {
            _cajaService = new CajaService(new CajaRepository());
            _sinpeService = new SinpeService(new SinpeRepository());
            _comercioService = new ComercioService(new ComercioRepository());

            //Instancia: Integración de bitacora para documentacion de cambios y errores.
            var context = new SINPE_Empresarial_DB();
            BitacoraInterface repo = new BitacoraRepository(context);
            _bitacoraService = new BitacoraService(repo);
        }

        // Método GET: Obtener todas las cajas de un comercio a corde al comercio seleccionado.
        public ActionResult Index(int idComercio)
        {
            if (User.IsInRole("Cajero"))
            {
                var fijo = GetComercioIdFromClaim();
                if (fijo == null) return new HttpStatusCodeResult(403);
                idComercio = fijo.Value;
            }

            var cajas = _cajaService.ObtenerCajasPorComercio(idComercio);
            ViewBag.IdComercio = idComercio;

            var comercio = _comercioService.ObtenerPorId(idComercio);
            ViewBag.NombreComercio = comercio?.Nombre ?? "Comercio no encontrado";

            return View(cajas);
        }

        // Método GET: Registro de una nueva caja para un comercio específico.
        public ActionResult Register(int idComercio)
        {
            if (User.IsInRole("Cajero"))
            {
                var fijo = GetComercioIdFromClaim();
                if (fijo == null) return new HttpStatusCodeResult(403);
                idComercio = fijo.Value; // forzar su comercio
            }

            var caja = new Caja { IdComercio = idComercio };
            return View(caja);
        }

        // Método POST: Aplicar registro de una nueva caja hacia el comercio seleccionado.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Caja caja)
        {
            if (User.IsInRole("Cajero"))
            {
                var fijo = GetComercioIdFromClaim();
                if (fijo == null) return new HttpStatusCodeResult(403);
                caja.IdComercio = fijo.Value; // forzar su comercio
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _cajaService.Registrar(caja);

                    var cajaDTO = CajaMapper.ToDTO(caja);
                    await _bitacoraService.RegistrarEvento("Caja", TiposDeEvento.Registrar,
                        $"Registro - Nueva caja: {cajaDTO.IdCaja} en comercio: {cajaDTO.ComercioId}",
                        datosPosteriores: cajaDTO);

                    return RedirectToAction("Index", new { idComercio = caja.IdComercio });
                }
            }
            catch (Exception ex)
            {
                await _bitacoraService.RegistrarEvento("Caja", TiposDeEvento.Error,
                    $"Error - Registro caja: {ex.Message}", datosPosteriores: caja, stackTrace: ex.ToString());
                ModelState.AddModelError("", ex.Message);
            }

            return View(caja);
        }

        // Método GET: Editar una caja existente.
        public ActionResult Editar(int id)
        {
            var caja = _cajaService.ObtenerPorId(id);
            if (caja == null) return HttpNotFound();

            if (User.IsInRole("Cajero"))
            {
                var fijo = GetComercioIdFromClaim();
                if (fijo == null || caja.IdComercio != fijo.Value)
                    return new HttpStatusCodeResult(403);
            }

            return View("Edit", caja);
        }

        // Método POST: Actualizar los datos de una caja existente.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Caja caja)
        {
            if (User.IsInRole("Cajero"))
            {
                var fijo = GetComercioIdFromClaim();
                if (fijo == null || caja.IdComercio != fijo.Value)
                    return new HttpStatusCodeResult(403); // no puede moverla a otro comercio
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var cajaOriginal = _cajaService.ObtenerPorId(caja.IdCaja);
                    var dtoAntes = CajaMapper.ToDTO(cajaOriginal);

                    _cajaService.Actualizar(caja);

                    var cajaActualizada = _cajaService.ObtenerPorId(caja.IdCaja);
                    var dtoDespues = CajaMapper.ToDTO(cajaActualizada);

                    await _bitacoraService.RegistrarEvento("Caja", TiposDeEvento.Editar,
                        $"Edición - Caja con ID: {caja.IdCaja} en comercio: {caja.IdComercio}",
                        datosAnteriores: dtoAntes, datosPosteriores: dtoDespues);

                    return RedirectToAction("Index", new { idComercio = caja.IdComercio });
                }
            }
            catch (Exception ex)
            {
                await _bitacoraService.RegistrarEvento("Caja", TiposDeEvento.Error,
                    $"Error - Editar caja: {ex.Message}", datosPosteriores: caja, stackTrace: ex.ToString());
                ModelState.AddModelError("", ex.Message);
            }

            return View("Edit", caja);
        }

        // Método GET: Seleccionar un comercio para mostrar cajas asociadas.
        public ActionResult SeleccionComercio()
        {
            if (User.IsInRole("Administrador"))
            {
                var comercios = _comercioService.ObtenerTodos();
                return View(comercios);
            }

            // Cajero: no elige, va directo a su comercio
            var fijo = GetComercioIdFromClaim();
            if (fijo == null) return new HttpStatusCodeResult(403);

            return RedirectToAction("Index", new { idComercio = fijo.Value });
        }


        // Llamar servicio de SINPE para consultar transacciones relacionadas
        private readonly SinpeService _sinpeService;

        // GET: Caja/Transacciones/{telefonoSINPE}
        public ActionResult Transacciones(string telefonoSINPE)
        {
            var transacciones = _sinpeService.ObtenerPorTelefonoCaja(telefonoSINPE);
            ViewBag.TelefonoSINPE = telefonoSINPE;
            return View(transacciones);
        }

        private int? GetComercioIdFromClaim()
        {
            var ci = User.Identity as ClaimsIdentity;
            // lee "commerceId" y, por compatibilidad, "ComercioId" si algún día existe
            var val = ci?.FindFirst("commerceId")?.Value ?? ci?.FindFirst("ComercioId")?.Value;
            return int.TryParse(val, out var id) ? id : (int?)null;
        }
    }
}