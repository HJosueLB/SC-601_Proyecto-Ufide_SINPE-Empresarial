// Llamar a las interfaces y entidades.
using SINPE_Empresarial.Domain.CatalogoDomain.Interfaces;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Infrastructure.CatalogoInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.ComercioInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SINPE_Empresarial.Controllers
{
    public class ComercioController : Controller
    {

        // Instancia: Servicio de comercio
        private readonly ComercioService _comercioService;

        // Instancia: Servicio de catagolo
        private readonly CatalogoInterface _catalogoService;

        public ComercioController()
        {
            // Instancia: Servicio con el repositorio de Comercio
            _comercioService = new ComercioService(new ComercioRepository());

            // Instancia: Servicio con el repositorio de Catalogo
            _catalogoService = new CatalogoRepository();

        }

        // Método GET: Obtener todo los registros de la entidad 'Comercio'.
        public ActionResult Index()
        {
            var comercios = _comercioService.ObtenerTodos();
            return View(comercios);
        }

        // Método GET: Registro de un nuevo comercio.
        public ActionResult Register()
        {
            ViewBag.TipoDeComercioId = new SelectList(_catalogoService.ObtenerTipoDeComercio(), "Id", "Nombre");
            ViewBag.TipoDeIdentificacionId = new SelectList(_catalogoService.ObtenerTipoDeIdentificacion(), "Id", "Nombre");

            return View();
        }

        // Método POST: Aplicar registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Comercio comercio)
        {
            // Validación del modelo mediante Try-Catch
            try
            {
                if (ModelState.IsValid)
                {
                    _comercioService.Registrar(comercio);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // En caso de error, recargar catálogos (TipoIdentificacion - TipoComercio)
            ViewBag.TipoDeComercioId = new SelectList(_catalogoService.ObtenerTipoDeComercio(), "Id", "Nombre", comercio.TipoDeComercioId);
            ViewBag.TipoDeIdentificacionId = new SelectList(_catalogoService.ObtenerTipoDeIdentificacion(), "Id", "Nombre", comercio.TipoDeIdentificacionId);

            return View(comercio);
        }

        // Método Post: Eliminar un comercio por ID.
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                _comercioService.Eliminar(id);
                return Json(new { success = true, mensaje = "Comercio eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = ex.Message });
            }
        }

        // Método GET: Editar un comercio por ID.
        public ActionResult Editar(int id)
        {
            var comercio = _comercioService.ObtenerPorId(id);

            if (comercio == null)
            {
                return HttpNotFound();
            }

            ViewBag.TipoDeComercioId = new SelectList(_catalogoService.ObtenerTipoDeComercio(), "Id", "Nombre", comercio.TipoDeComercioId);
            ViewBag.TipoDeIdentificacionId = new SelectList(_catalogoService.ObtenerTipoDeIdentificacion(), "Id", "Nombre", comercio.TipoDeIdentificacionId);

            return View("Edit", comercio);
        }

        // Método POST: Aplicar edición de comercio.   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Comercio comercio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _comercioService.Actualizar(comercio);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewBag.TipoDeComercioId = new SelectList(_catalogoService.ObtenerTipoDeComercio(), "Id", "Nombre", comercio.TipoDeComercioId);
            ViewBag.TipoDeIdentificacionId = new SelectList(_catalogoService.ObtenerTipoDeIdentificacion(), "Id", "Nombre", comercio.TipoDeIdentificacionId);

            return View("Edit", comercio);
        }

        // Método GET: Detalle de un comercio por ID.
        public ActionResult Detalles(int id)
        {
            var comercio = _comercioService.ObtenerPorId(id);
            if (comercio == null) return HttpNotFound();

            return View("Details", comercio);
        }


    }
}
