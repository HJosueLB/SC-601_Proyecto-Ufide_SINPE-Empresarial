using SINPE_Empresarial.Domain.BitacoraDomain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SINPE_Empresarial.Services.Bitacora
{
    // Interface de Bitacora para registrar eventos generados por el sistema
    public interface IBitacoraManager
    {
        Task RegistrarEvento(
            string tablaDeEvento,
            string tipoDeEvento,
            string descripcionDeEvento,
            object datosAnteriores = null,
            object datosPosteriores = null,
            string stackTrace = null);

        Task<IEnumerable<BitacoraEvento>> ObtenerEventos();
    }
}
