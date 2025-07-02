using SINPE_Empresarial.Domain.CajaDomain.Entities;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Infrastructure.CajaInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.ComercioInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.SinpeInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using System;
using System.Web.Mvc;

namespace SINPE_Empresarial.Controllers
{
    public class CajaController : Controller
    {
        // Instancia: Servicio de caja
        private readonly CajaService _cajaService;

        // Instancia: Servicio de Comercio
        private readonly ComercioService _comercioService;

        // Constructor sin parámetros (por compatibilidad con MVC sin IoC)
        public CajaController()
        {
            _cajaService = new CajaService(new CajaRepository());
            _sinpeService = new SinpeService(new SinpeRepository());
            _comercioService = new ComercioService(new ComercioRepository());
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
        public ActionResult Register(Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cajaService.Registrar(caja);
                    return RedirectToAction("Index", new { idComercio = caja.IdComercio });
                }
            }
            catch (Exception ex)
            {
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
        public ActionResult Editar(Caja caja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cajaService.Actualizar(caja);
                    return RedirectToAction("Index", new { idComercio = caja.IdComercio });
                }
            }
            catch (Exception ex)
            {
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