using SINPE_Empresarial.Domain.ConfiguracionDomain.Interfaces;
using SINPE_Empresarial.Services.Configuracion.DTOs;
using SINPE_Empresarial.Domain.ConfiguracionDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Services.Configuracion
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

        public void Actualizar(SINPE_Empresarial.Domain.ConfiguracionDomain.Entities.Configuracion c)
        {
            _repositorio.Actualizar(c);

        }

        public void Agregar(SINPE_Empresarial.Domain.ConfiguracionDomain.Entities.Configuracion c)
        {
            _repositorio.Agregar(c);
        }

        public SINPE_Empresarial.Domain.ConfiguracionDomain.Entities.Configuracion ObtenerPorId(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }

        public bool ExisteConfiguracionPorComercio(int idComercio)
        {
            return _repositorio.ExisteConfiguracionPorComercio(idComercio);
        }
    }
}