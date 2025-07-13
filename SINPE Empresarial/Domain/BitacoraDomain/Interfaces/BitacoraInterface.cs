using SINPE_Empresarial.Domain.BitacoraDomain.Entities;
using SINPE_Empresarial.Domain.BitacoraDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SINPE_Empresarial.Domain.BitacoraDomain.Interfaces
{
    public interface BitacoraInterface
    {
        Task RegistrarEvento(BitacoraEvento evento);
        Task<IEnumerable<BitacoraEvento>> ObtenerTodosLosEventos();
    }
}



