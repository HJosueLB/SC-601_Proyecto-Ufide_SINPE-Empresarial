using SINPE_Empresarial.Domain.BitacoraDomain.Entities;
using SINPE_Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE_Empresarial.Domain.CajaDomain.Entities;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Infrastructure.BitacoraInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.CajaInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.ComercioInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.SinpeInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using SINPE_Empresarial.Services.Bitacora;
using SINPE_Empresarial.Services.Bitacora.Mappers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SINPE_Empresarial.Controllers
{
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
            var cajas = _cajaService.ObtenerCajasPorComercio(idComercio);
            ViewBag.IdComercio = idComercio;

            var comercio = _comercioService.ObtenerPorId(idComercio);
            ViewBag.NombreComercio = comercio?.Nombre ?? "Comercio no encontrado";

            return View(cajas);
        }

        // Método GET: Registro de una nueva caja para un comercio específico.
        public ActionResult Register(int idComercio)
        {
            var caja = new Caja { IdComercio = idComercio };
            return View(caja);
        }

        // Método POST: Aplicar registro de una nueva caja hacia el comercio seleccionado.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cajaService.Registrar(caja);

                    // DTO para la bitácora
                    var cajaDTO = CajaMapper.ToDTO(caja);

                    // Método asincrónico await: Registar evento de creación de comercio
                    await _bitacoraService.RegistrarEvento("Caja",TiposDeEvento.Registrar,
                        $"Registro - Nueva caja: {cajaDTO.IdCaja} en comercio: {cajaDTO.ComercioId}",datosPosteriores: cajaDTO);


                    return RedirectToAction("Index", new { idComercio = caja.IdComercio });
                }
            }
            catch (Exception ex)
            {
                // Método asincrónico await: Registar evento en caso de error
                await _bitacoraService.RegistrarEvento("Caja",TiposDeEvento.Error,
                    $"Error - Registro caja: {ex.Message}", datosPosteriores: caja,stackTrace: ex.ToString());

                ModelState.AddModelError("", ex.Message);
            }

            return View(caja);
        }

        // Método GET: Editar una caja existente.
        public ActionResult Editar(int id)
        {
            var caja = _cajaService.ObtenerPorId(id);
            if (caja == null)
                return HttpNotFound();

            return View("Edit", caja);
        }

        // Método POST: Actualizar los datos de una caja existente.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Obtener datos anteriores
                    var cajaOriginal = _cajaService.ObtenerPorId(caja.IdCaja);
                    var dtoAntes = CajaMapper.ToDTO(cajaOriginal);

                    _cajaService.Actualizar(caja);

                    // Obtener datos nuevos
                    var cajaActualizada = _cajaService.ObtenerPorId(caja.IdCaja);
                    var dtoDespues = CajaMapper.ToDTO(cajaActualizada);

                    // Método asincrónico await: Registar evento de edición de comercio
                    await _bitacoraService.RegistrarEvento("Caja",TiposDeEvento.Editar,
                        $"Edición - Caja con ID: {caja.IdCaja} en comercio: {caja.IdComercio}",datosAnteriores: dtoAntes,datosPosteriores: dtoDespues );

                    return RedirectToAction("Index", new { idComercio = caja.IdComercio });
                }
            }
            catch (Exception ex)
            {
                // Método asincrónico await: Registar evento en caso de error
                await _bitacoraService.RegistrarEvento("Caja",TiposDeEvento.Error,
                    $"Error - Editar caja: {ex.Message}",datosPosteriores: caja, stackTrace: ex.ToString());

                ModelState.AddModelError("", ex.Message);
            }

            return View("Edit", caja);
        }

        // Método GET: Seleccionar un comercio para mostrar cajas asociadas.
        public ActionResult SeleccionComercio()
        {
            var comercioService = new ComercioService(new ComercioRepository());
            var comercios = comercioService.ObtenerTodos();
            return View(comercios);
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

    }

}