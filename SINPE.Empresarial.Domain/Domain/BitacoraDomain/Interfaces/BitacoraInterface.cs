using SINPE.Empresarial.Domain.BitacoraDomain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SINPE.Empresarial.Domain.BitacoraDomain.Interfaces
{
    public interface BitacoraInterface
    {
        Task RegistrarEvento(BitacoraEvento evento);
        Task<IEnumerable<BitacoraEvento>> ObtenerTodosLosEventos();
    }
}



