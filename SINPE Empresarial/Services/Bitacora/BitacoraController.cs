using SINPE_Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE_Empresarial.Infrastructure.BitacoraInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SINPE_Empresarial.Services.Bitacora
{
    public class BitacoraController : Controller
    {
        private readonly BitacoraService _bitacoraService;

        public BitacoraController()
        {
            var context = new SINPE_Empresarial_DB();
            BitacoraInterface repositoryBitacora = new BitacoraRepository(context);
            _bitacoraService = new BitacoraService(repositoryBitacora);
        }

        // GET: Bitacora
        public async Task<ActionResult> Index()
        {
            var eventos = await _bitacoraService.ObtenerEventos();
            return View(eventos);
        }
    }
}

