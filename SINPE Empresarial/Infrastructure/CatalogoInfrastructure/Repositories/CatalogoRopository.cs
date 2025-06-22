using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Llamar a las interfaces y entidad requeridas.
using SINPE_Empresarial.Domain.CatalogoDomain.Interfaces;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;

namespace SINPE_Empresarial.Infrastructure.CatalogoInfrastructure.Repositories
{
    public class CatalogoRepository : CatalogoInterface
    {
        // Atributo: Contexto de la base de datos para acceder a las tablas de la base de datos.
        private readonly SINPE_Empresarial_DB _context;

        // Constructor: Inicializa el contexto de la base de datos.
        public CatalogoRepository()
        {
            _context = new SINPE_Empresarial_DB();
        }

        // Método: Listar los tipos de identificación y tipos de comercio existentes en la base de datos.
        public IEnumerable<TipoDeIdentificacion> ObtenerTipoDeIdentificacion()
        {
            return _context.TipoDeIdentificacion.ToList();
        }

        // Método: Listar los tipos de comercio existentes en la base de datos.
        public IEnumerable<TipoDeComercio> ObtenerTipoDeComercio()
        {
            return _context.TipoDeComercio.ToList();
        }
    }
}