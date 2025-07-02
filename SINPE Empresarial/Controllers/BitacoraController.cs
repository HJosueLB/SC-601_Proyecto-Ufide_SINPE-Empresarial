using SINPE_Empresarial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SINPE_Empresarial.Controllers
{
    public class BitacoraController : Controller
    {
        private readonly BitacoraService _bitacoraService;

        public BitacoraController(BitacoraService bitacoraService)
        {
            _bitacoraService = bitacoraService;
        }

        // GET: Bitacora
        public async Task<ActionResult> Index()
        {
            var eventos = await _bitacoraService.GetBitacoraEventos();
            return View(eventos);
        }
    }
}

