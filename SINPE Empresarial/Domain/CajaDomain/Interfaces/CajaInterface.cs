using SINPE_Empresarial.Domain.CajaDomain.Entities;
using System.Collections.Generic;

namespace SINPE_Empresarial.Domain.CajaDomain.Intefaces
{
    public interface CajaInterface
    {
        // Método: Listar las cajas asociadas a un comercio específico.
        IEnumerable<Caja> ObtenerPorComercio(int idComercio);

        // Método: Obtener una caja específica por ID.
        Caja ObtenerPorId(int id);

        // Método: Registrar una nueva caja.
        void Registrar(Caja caja);

        // Método: Actualizar los datos de una caja existente.
        void Actualizar(Caja caja);

        // Método: Validar si ya existe una caja.
        bool ExisteNombreEnComercio(string nombre, int idComercio, int? idCaja = null);

        // Método: Validar si existe una caja activa con el mismo número de teléfono.
        bool ExisteTelefonoActivo(string telefonoSINPE, int? idCaja = null);


    }
}
