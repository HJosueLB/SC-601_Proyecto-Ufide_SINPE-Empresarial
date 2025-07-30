using SINPE_Empresarial.Domain.UsuarioDomain.Entities;
using SINPE_Empresarial.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using SINPE_Empresarial.Infrastructure.UsuarioInfrastructure.Repositories;

namespace SINPE_Empresarial.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly SINPE_Empresarial_DB _context;

        public UsuarioController()
        {
            _context = new SINPE_Empresarial_DB();
            _usuarioService = new UsuarioService(new UsuarioRepository());
        }

        public ActionResult Index()
        {
            var usuarios = _usuarioService.ObtenerTodos();
            return View(usuarios);
        }

        public ActionResult Crear()
        {
            ViewBag.Comercios = new SelectList(_context.Comercio.ToList(), "IdComercio", "Nombre");
            return View(new Usuario());
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Comercios = new SelectList(_context.Comercio.ToList(), "IdComercio", "Nombre", usuario.IdComercio);
                return View(usuario);
            }

            // Validación: No permitir identificaciones duplicadas
            var existe = _usuarioService.ObtenerTodos().Any(u => u.Identificacion == usuario.Identificacion);

            if (existe)
            {
                TempData["Error"] = "Ya existe un usuario con esta identificación.";
                return RedirectToAction("Crear");
            }

            usuario.FechaDeRegistro = DateTime.Now;
            usuario.Estado = true;

            _usuarioService.Registrar(usuario);
            return RedirectToAction("Index");
        }
     
        public ActionResult Editar(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);
            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            // Verificar que la identificación no esté duplicada (excluyendo el usuario actual)
            var duplicado = _usuarioService.ObtenerTodos()
                .Any(u => u.Identificacion == usuario.Identificacion && u.IdUsuario != usuario.IdUsuario);

            if (duplicado)
            {
                ModelState.AddModelError("Identificacion", "Ya existe un usuario con esta identificación.");
                return View(usuario);
            }

            usuario.FechaDeModificacion = DateTime.Now;
            _usuarioService.Actualizar(usuario);

            return RedirectToAction("Index");
        }

    }
}
