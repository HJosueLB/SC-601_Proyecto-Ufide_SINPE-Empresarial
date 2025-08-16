using SINPE.Empresarial.Domain.ComercioDomain.Entities;

using System.Collections.Generic;

namespace SINPE.Empresarial.Domain.ComercioDomain.Interfaces
{
    public interface ComercioInterface
    {
        // Método: Listar los comercios existentes en la base de datos.
        IEnumerable<Comercio> ObtenerTodos();

        // Método: Obtener comercios por ID desde la base de datos.
        Comercio ObtenerPorId(int id);

        // Método: Registra un nuevo comercio en la base de datos.
        void Registrar(Comercio comercio);

        // Método: Actualiza los datos de un comercio existente.
        void Actualizar(Comercio comercio);

        // Método: Elimina el comercio especificado por ID.
        void Eliminar(int id);
    }
}
