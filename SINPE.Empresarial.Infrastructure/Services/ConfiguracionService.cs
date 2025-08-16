using SINPE.Empresarial.Domain.ConfiguracionDomain.Interfaces;
using SINPE.Empresarial.Domain.ConfiguracionDomain;

using System.Collections.Generic;

namespace SINPE.Empresarial.Infrastructure.Services
{
    public class ConfiguracionService
    {
        private readonly ConfiguracionInterface _repositorio;

        public ConfiguracionService(ConfiguracionInterface repositorio)
        {
            _repositorio = repositorio;
        }
        public IEnumerable<ConfiguracionListadoDto> ListarConfiguraciones(){
            return _repositorio.ListarConfiguraciones();
        }

        public void Actualizar(SINPE.Empresarial.Domain.ConfiguracionDomain.Entities.Configuracion c)
        {
            _repositorio.Actualizar(c);
        }

        public void Agregar(SINPE.Empresarial.Domain.ConfiguracionDomain.Entities.Configuracion c)
        {
            _repositorio.Agregar(c);
        }

        public SINPE.Empresarial.Domain.ConfiguracionDomain.Entities.Configuracion ObtenerPorId(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }

        public bool ExisteConfiguracionPorComercio(int idComercio)
        {
            return _repositorio.ExisteConfiguracionPorComercio(idComercio);
        }
    }
}