using SINPE.Empresarial.Domain.ComercioDomain.Entities;

using System.Collections.Generic;

namespace SINPE.Empresarial.Domain.CatalogoDomain.Interfaces
{
    public interface CatalogoInterface
    {
        // Método: Listar los tipos de identificación existentes en la base de datos.
        IEnumerable<TipoDeIdentificacion> ObtenerTipoDeIdentificacion();

        // Método: Listar los tipos de comercios existentes en la base de datos.
        IEnumerable<TipoDeComercio> ObtenerTipoDeComercio();
    }
}
