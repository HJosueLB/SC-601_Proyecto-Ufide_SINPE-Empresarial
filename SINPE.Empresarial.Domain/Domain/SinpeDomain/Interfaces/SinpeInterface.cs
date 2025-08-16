using SINPE.Empresarial.Domain.SinpeDomain.Entities;

using System.Collections.Generic;

namespace SINPE.Empresarial.Domain.SinpeDomain.Interfaces
{
    public interface SinpeInterface
    {
        // Método: Registra un nuevo sinpe en la base de datos.
        void Registrar(Sinpe sinpe);

        // Método: Obtiene todos los sinpes registrados por teléfono de caja.
        IEnumerable<Sinpe> ObtenerPorTelefonoCaja(string telefonoSINPE);

    }
}