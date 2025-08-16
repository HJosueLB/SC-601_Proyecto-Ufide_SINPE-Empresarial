using SINPE.Empresarial.Domain.ConfiguracionDomain.Entities;

using System.Collections.Generic;

namespace SINPE.Empresarial.Domain.ConfiguracionDomain.Interfaces
{
    public interface ConfiguracionInterface
    {
        IEnumerable<Configuracion> ObtenerTodos();
        void Actualizar(Configuracion c);
        void Agregar(Configuracion c);
        IEnumerable<ConfiguracionListadoDto> ListarConfiguraciones();
        Configuracion ObtenerPorId(int id);
        bool ExisteConfiguracionPorComercio(int idComercio);
    }
}
