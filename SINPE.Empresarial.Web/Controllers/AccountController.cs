using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SINPE.Empresarial.Infrastructure.Identity;     
using SINPE.Empresarial.Infrastructure.Repositories;   
using SINPE.Empresarial.Web.Auth;                     
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SINPE.Empresarial.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        private IAuthenticationManager Auth => HttpContext.GetOwinContext().Authentication;

        private static readonly string[] RolesPermitidos = new[] { "Administrador", "Cajero" };

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await UserManager.FindAsync(vm.Email, vm.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Credenciales inválidas.");
                return View(vm);
            }

            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            if (await UserManager.IsInRoleAsync(user.Id, "Cajero"))
            {
                var urepo = new UsuarioRepository();

                var usuario = urepo.ObtenerPorIdNetUser(user.Id) ?? urepo.ObtenerPorCorreo(user.Email);

                if (usuario == null || usuario.Estado == false)
                {
                    ModelState.AddModelError("", "Usuario de negocio no habilitado o inexistente.");
                    return View(vm);
                }

                if (!usuario.IdNetUser.HasValue)
                    urepo.EnlazarIdentity(usuario.IdUsuario, user.Id);

                identity.AddClaim(new Claim("usuarioId", usuario.IdUsuario.ToString()));
                if (usuario.IdComercio > 0)
                    identity.AddClaim(new Claim("commerceId", usuario.IdComercio.ToString()));
            }
            else
            {
                identity.AddClaim(new Claim("admin", "true"));
            }

            Auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Auth.SignIn(new AuthenticationProperties { IsPersistent = vm.RememberMe }, identity);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            ViewBag.Roles = new System.Web.Mvc.SelectList(
                RolesPermitidos.Select(r => new { Value = r, Text = r }),
                "Value", "Text"
            );
            return View(new RegisterViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel vm)
        {
            // --- Normalización de entrada -----------------------------------------
            var postedEmail = (vm.Email ?? string.Empty).Trim().ToLowerInvariant();

            // a veces el binder no llena vm.Rol (o llega con espacios/valores raros)
            var rawRol = (vm.Rol ?? Request["Rol"] ?? string.Empty).Trim();

            // repoblar SIEMPRE el combo de roles
            ViewBag.Roles = new System.Web.Mvc.SelectList(
                RolesPermitidos.Select(r => new { Value = r, Text = r }),
                "Value", "Text", rawRol
            );

            if (!ModelState.IsValid) return View(vm);

            // --- Mapeo/validación súper robusta del rol ---------------------------
            // aceptamos alias por si el select estuviera mal configurado
            var roleMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["administrador"] = "Administrador",
                ["admin"] = "Administrador",
                ["1"] = "Administrador",

                ["cajero"] = "Cajero",
                ["2"] = "Cajero"
            };

            // intentamos normalizar al nombre oficial
            string normalizedRol = null;
            if (roleMap.TryGetValue(rawRol, out var mapped))
                normalizedRol = mapped;
            else
                normalizedRol = RolesPermitidos
                    .FirstOrDefault(r => r.Equals(rawRol, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(normalizedRol))
            {
                // opcional: ver qué está llegando realmente
                System.Diagnostics.Debug.WriteLine(">>> ROL POSTED (raw): '" + rawRol + "'");
                ModelState.AddModelError("Rol", "Rol inválido (use Administrador o Cajero).");
                return View(vm);
            }

            vm.Rol = normalizedRol; // ya normalizado

            // --- Reglas de negocio: enlace con Usuarios si es Cajero --------------
            int? idUsuarioNegocio = null;
            if (vm.Rol == "Cajero")
            {
                var urepo = new UsuarioRepository();
                var usuario = urepo.ObtenerPorCorreo(postedEmail);
                if (usuario == null)
                {
                    ModelState.AddModelError("", "Este correo no está registrado como usuario (Cajero) del comercio.");
                    return View(vm);
                }
                if (!usuario.Estado)
                {
                    ModelState.AddModelError("", "El usuario de negocio está inactivo.");
                    return View(vm);
                }
                idUsuarioNegocio = usuario.IdUsuario;
            }

            // --- Evitar duplicados en Identity ------------------------------------
            var existing = await UserManager.FindByNameAsync(postedEmail);
            if (existing != null)
            {
                ModelState.AddModelError("", "Ya existe una cuenta con este correo.");
                return View(vm);
            }

            // --- Crear usuario en Identity ----------------------------------------
            var user = new ApplicationUser
            {
                UserName = postedEmail,
                Email = postedEmail,
                EmailConfirmed = true
            };
            var result = await UserManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError("", e);
                return View(vm);
            }

            // --- Asegurar rol y asignar -------------------------------------------
            if (!await RoleManager.RoleExistsAsync(vm.Rol))
                await RoleManager.CreateAsync(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(vm.Rol));

            await UserManager.AddToRoleAsync(user.Id, vm.Rol);

            // --- Enlazar con tu tabla Usuarios si es Cajero -----------------------
            if (vm.Rol == "Cajero" && idUsuarioNegocio.HasValue)
            {
                var urepo = new UsuarioRepository();
                urepo.EnlazarIdentity(idUsuarioNegocio.Value, user.Id); // tu repo usa string Guid
            }

            // --- Autologin ---------------------------------------------------------
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            Auth.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            return RedirectToAction("Index", "Home");
        }



        [Authorize]
        public ActionResult Logout()
        {
            Auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}
