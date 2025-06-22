using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Llamar a las interfaces y entidad requeridas.
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
namespace SINPE_Empresarial.Domain.CatalogoDomain.Interfaces
{
    public interface CatalogoInterface
    {
        // Método: Listar los tipos de identificación existentes en la base de datos.
        IEnumerable<TipoDeIdentificacion> ObtenerTipoDeIdentificacion();

        // Método: Listar los tipos de comercios existentes en la base de datos.
        IEnumerable<TipoDeComercio> ObtenerTipoDeComercio();
    }
}
