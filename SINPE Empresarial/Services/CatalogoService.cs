using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Llamar a las interfaces y entidad requeridas.
using SINPE_Empresarial.Domain.CatalogoDomain.Interfaces;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;

namespace SINPE_Empresarial.Services
{
    public class CatalogoService
    {
        // Atributo: Repositorio de catálogo que implementa la interfaz CatalogoInterface.
        private readonly CatalogoInterface _repositorio;

        // Constructor: Inicializa el servicio de catálogo con el repositorio.
        public CatalogoService(CatalogoInterface repositorio)
        {
            _repositorio = repositorio;
        }

        // Métodos: Listar los tipos de identificación y comercio existentes en la base de datos.
        public IEnumerable<TipoDeIdentificacion> ObtenerTiposDeIdentificacion()
        {
            return _repositorio.ObtenerTipoDeIdentificacion();
        }

        // Método: Listar los tipos de comercio existentes en la base de datos.
        public IEnumerable<TipoDeComercio> ObtenerTiposDeComercio()
        {
            return _repositorio.ObtenerTipoDeComercio();
        }
    }
}