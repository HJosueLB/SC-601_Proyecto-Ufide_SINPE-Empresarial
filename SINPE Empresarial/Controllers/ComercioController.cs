// Llamar a las interfaces y entidades.
using Microsoft.Win32;
using SINPE_Empresarial.Domain.BitacoraDomain.Entities;
using SINPE_Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE_Empresarial.Domain.CatalogoDomain.Interfaces;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Infrastructure.BitacoraInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.CatalogoInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.ComercioInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using SINPE_Empresarial.Services.Bitacora;
using SINPE_Empresarial.Services.Bitacora.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // Instancia: Servicio de bitacora
        private readonly BitacoraService _bitacoraService;

        public ComercioController()
        {
            // Instancia: Servicio con el repositorio de Comercio
            _comercioService = new ComercioService(new ComercioRepository());

            // Instancia: Servicio con el repositorio de Catalogo
            _catalogoService = new CatalogoRepository();

            //Instancia: Integración de bitacora para documentacion de cambios y errores.
            var context = new SINPE_Empresarial_DB();
            BitacoraInterface repo = new BitacoraRepository(context);
            _bitacoraService = new BitacoraService(repo);
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
        public async Task<ActionResult> Register(Comercio comercio)
        {
            // Validación del modelo mediante Try-Catch
            try
            {
                if (ModelState.IsValid)
                {
                    _comercioService.Registrar(comercio);

                    // Obtener el DTO del comercio para para la bitácora.
                    var comercioDTO = ComercioMapper.ToDTO(comercio);

                    // Método asincrónico await: Registar evento de creación de comercio
                    await _bitacoraService.RegistrarEvento("Comercio",TiposDeEvento.Registrar,
                        $"Registro - Nuevo comercio: {comercioDTO.IdComercio}",datosPosteriores: comercioDTO);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                // Método sincrónico wait: Registar evento en caso de error
                _bitacoraService.RegistrarEvento("Comercio",TiposDeEvento.Error,
                    $"Error - Registro comercio: {ex.Message}",
                    datosPosteriores: comercio,stackTrace: ex.StackTrace).Wait();


                ModelState.AddModelError("", ex.Message);
            }

            // En caso de error, recargar catálogos (TipoIdentificacion - TipoComercio)
            ViewBag.TipoDeComercioId = new SelectList(_catalogoService.ObtenerTipoDeComercio(), "Id", "Nombre", comercio.TipoDeComercioId);
            ViewBag.TipoDeIdentificacionId = new SelectList(_catalogoService.ObtenerTipoDeIdentificacion(), "Id", "Nombre", comercio.TipoDeIdentificacionId);

            return View(comercio);
        }

        // Método Post: Eliminar un comercio por ID.

        [HttpPost]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                var comercio = _comercioService.ObtenerPorId(id);
                    if (comercio == null)
                        throw new Exception("El comercio a eliminar no se encuentra o ya fue eliminado.");

                var comercioDTO = ComercioMapper.ToDTO(comercio);

                _comercioService.Eliminar(id);

                await _bitacoraService.RegistrarEvento("Comercio", TiposDeEvento.Eliminar,
                    $"Eliminar - Comercio con ID: {id}",
                    datosAnteriores: comercioDTO
                );

                return Json(new { success = true, mensaje = "Comercio eliminado correctamente." });
            }
            catch (Exception ex)
            {
                await _bitacoraService.RegistrarEvento("Comercio", TiposDeEvento.Error,
                    $"Error - Eliminar comercio: {id}.", stackTrace: ex.ToString());

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
        public async Task<ActionResult> Editar(Comercio comercio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Obtener datos originales antes de la actualización
                    var comercioOriginal = _comercioService.ObtenerPorId(comercio.IdComercio);
                    var datosOriginales = ComercioMapper.ToDTO(comercioOriginal);

                    // Actualizar comercio (si tu método de servicio es sincrónico, lo puedes mantener así)
                    _comercioService.Actualizar(comercio);

                    // Obtener los datos actualizados después de guardar
                    var comercioActualizado = _comercioService.ObtenerPorId(comercio.IdComercio);
                    var datosActualizados = ComercioMapper.ToDTO(comercioActualizado);

                    // Método asincrónico await: Registar evento de edición de comercio
                    await _bitacoraService.RegistrarEvento("Comercio",TiposDeEvento.Editar,
                        $"Actualización de datos: {comercio.IdComercio}",datosAnteriores: datosOriginales,datosPosteriores: datosActualizados);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Obtener el DTO del comercio para para la bitácora.
                var comercioDTO = ComercioMapper.ToDTO(comercio);

                // Método asincrónico await: Registar evento en caso de error
                await _bitacoraService.RegistrarEvento("Comercio",TiposDeEvento.Error,
                    $"Error - Editar comercio: {ex.Message}",datosPosteriores: comercioDTO,stackTrace: ex.ToString()
                );

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
