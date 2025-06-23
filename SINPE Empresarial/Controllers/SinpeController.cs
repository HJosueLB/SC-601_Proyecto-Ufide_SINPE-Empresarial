using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Domain.SinpeDomain.Entities;
// Llamar a las interfaces y entidades de Sinpe.
using SINPE_Empresarial.Domain.SinpeDomain.Interfaces;
using SINPE_Empresarial.Infrastructure.ComercioInfrastructure.Repositories;
using SINPE_Empresarial.Infrastructure.SinpeInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SINPE_Empresarial.Controllers
{
    public class SinpeController : Controller
    {

        // // Instancia: Servicio de comercio
        private readonly SinpeService _sinpeService;

        public SinpeController() {
            _sinpeService = new SinpeService(new SinpeRepository());
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Sinpe sinpe)
        {
            if (!ModelState.IsValid)
                return View(sinpe);

            // Validación simulada: verificar que la caja exista y esté activa
            bool cajaValida = VerificarCaja(sinpe.TelefonoDestinatario);

            if (!cajaValida)
            {
                ModelState.AddModelError("", "No se puede registrar el pago: la caja está inactiva o no tiene teléfono registrado.");
                return View(sinpe);
            }

            sinpe.FechaDeRegistro = DateTime.Now;
            sinpe.Estado = false; // No sincronizado

            _sinpeService.Registrar(sinpe);
            return RedirectToAction("Register");
        }

        private bool VerificarCaja(string telefono)
        {
            // Aquí se debe consultar la BD algo asi una vez que este lo de cajas
            // Caja caja = _context.Cajas.FirstOrDefault(c => c.TelefonoSINPE == telefono && c.Estado);
            return true;
        }
    }
}
