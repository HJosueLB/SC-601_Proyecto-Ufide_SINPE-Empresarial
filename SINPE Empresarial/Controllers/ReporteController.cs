using SINPE_Empresarial.Domain.BitacoraDomain.Entities;
using SINPE_Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE_Empresarial.Infrastructure.BitacoraInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using SINPE_Empresarial.Services.Bitacora;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SINPE_Empresarial.Controllers
{
    public class ReporteController : Controller
    {

        // Instancia: Servicio de Reporteria
        private readonly ReporteService _reporteService;

        // Instancia: Servicio de Bitacora
        private readonly BitacoraService _bitacoraService;

        // Constructor sin parámetros (por compatibilidad con MVC sin IoC)
        public ReporteController()
        {
            _reporteService = new ReporteService();

            //Instancia: Integración de bitacora para documentacion de cambios y errores.
            var context = new SINPE_Empresarial_DB();
            BitacoraInterface repo = new BitacoraRepository(context);
            _bitacoraService = new BitacoraService(repo);
        }

        // Método GET: Obtener todos los reportes mensuales
        public ActionResult Index()
        {
            var reportes = _reporteService.ObtenerTodos();
            return View(reportes);
        }

        // Método POST: Generar o actualizar los reportes mensuales
        [HttpPost]
        public async Task<ActionResult> Generar()
        {
            _reporteService.GenerarReportesMensuales();

            await _bitacoraService.RegistrarEvento(
                "ReporteMensual",
                TiposDeEvento.Registrar,
                "Se generaron/actualizaron los reportes mensuales."
            );

            return RedirectToAction("Index");
        }
    }
}