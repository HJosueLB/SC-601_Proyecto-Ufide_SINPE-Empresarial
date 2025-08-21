using SINPE.Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE.Empresarial.Infrastructure.Data;
using SINPE.Empresarial.Infrastructure.Repositories;
using SINPE.Empresarial.Infrastructure.Services;

using System.Threading.Tasks;
using System.Web.Mvc;

namespace SINPE.Empresarial.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
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