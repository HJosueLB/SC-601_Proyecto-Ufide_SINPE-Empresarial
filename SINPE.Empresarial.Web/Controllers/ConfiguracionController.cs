using SINPE.Empresarial.Domain.ConfiguracionDomain.Entities;
using SINPE.Empresarial.Infrastructure.Repositories;
using SINPE.Empresarial.Infrastructure.Services;

using System;
using System.Web.Mvc;

namespace SINPE.Empresarial.Web.Controllers
{
    public class ConfiguracionController : Controller
    {

        private readonly ConfiguracionService _configuracionService;
        private readonly ComercioService _comercioService;

        public ConfiguracionController()
        {
            _configuracionService = new ConfiguracionService(new ConfiguracionRepository());
            _comercioService = new ComercioService(new ComercioRepository());
        }


        // GET: Configuracion
        public ActionResult Index()
        {
            var lista = _configuracionService.ListarConfiguraciones();
            return View(lista);
        }

        public ActionResult Editar(int id)
        {
            var configuracion = _configuracionService.ObtenerPorId(id);

            if (configuracion == null)
            {
                return HttpNotFound();
            }

            return View(configuracion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                var configuracionBD = _configuracionService.ObtenerPorId(configuracion.IdConfiguracion);

                if (configuracionBD == null)
                {
                    return HttpNotFound();
                }

                // Solo actualiza los campos permitidos
                configuracionBD.TipoConfiguracion = configuracion.TipoConfiguracion;
                configuracionBD.Comision = configuracion.Comision;
                configuracionBD.Estado = configuracion.Estado;
                configuracionBD.FechaDeModificacion = DateTime.Now;

                _configuracionService.Actualizar(configuracionBD);

                return RedirectToAction("index");
            }

            return View(configuracion);
        }

        public ActionResult Agregar()
        {
            ViewBag.ListaComercios = new SelectList(_comercioService.ObtenerTodos(), "IdComercio", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                var existe = _configuracionService.ExisteConfiguracionPorComercio(configuracion.IdComercio);

                if (existe)
                {
                    ModelState.AddModelError("", "Ya existe una configuración para el comercio seleccionado.");
                    ViewBag.ListaComercios = new SelectList(_comercioService.ObtenerTodos(), "IdComercio", "Nombre", configuracion.IdComercio);
                    return View(configuracion);
                }

                configuracion.FechaDeRegistro = DateTime.Now;
                configuracion.Estado = true;
                configuracion.FechaDeModificacion = null;

                _configuracionService.Agregar(configuracion);

                return RedirectToAction("Index");
            }

            ViewBag.ListaComercios = new SelectList(_comercioService.ObtenerTodos(), "IdComercio", "Nombre", configuracion.IdComercio);
            return View(configuracion);
        }

    }
}