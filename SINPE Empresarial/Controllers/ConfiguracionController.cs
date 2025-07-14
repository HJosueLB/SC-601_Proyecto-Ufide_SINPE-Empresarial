using SINPE_Empresarial.Infrastructure.ConfiguracionInfraestructure.Repositories;
using SINPE_Empresarial.Infrastructure.SinpeInfrastructure.Repositories;
using SINPE_Empresarial.Services;
using SINPE_Empresarial.Services.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SINPE_Empresarial.Controllers
{
    public class ConfiguracionController : Controller
    {

        private readonly ConfiguracionService configuracionService;

        public ConfiguracionController()
        {
            configuracionService = new ConfiguracionService(new ConfiguracionRepository());
        }


        // GET: Configuracion
        public ActionResult Index()
        {
            var lista = configuracionService.ListarConfiguraciones();
            return View(lista);
        }
    }
}