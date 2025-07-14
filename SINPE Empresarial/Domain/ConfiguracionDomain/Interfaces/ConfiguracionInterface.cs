using SINPE_Empresarial.Domain.ConfiguracionDomain.Entities;
using SINPE_Empresarial.Services.Configuracion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINPE_Empresarial.Domain.ConfiguracionDomain.Interfaces
{
    public interface ConfiguracionInterface
    {
        IEnumerable<Configuracion> ObtenerTodos();
        void Actualizar(Configuracion c);
        void Agregar(Configuracion c);
        IEnumerable<ConfiguracionListadoDto> ListarConfiguraciones();
    }
}
