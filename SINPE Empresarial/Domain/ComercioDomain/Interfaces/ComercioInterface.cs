using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Llamar carpeta de clases Entities de Comercio.
using SINPE_Empresarial.Domain.ComercioDomain.Entities;

namespace SINPE_Empresarial.Domain.ComercioDomain.Interfaces
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
