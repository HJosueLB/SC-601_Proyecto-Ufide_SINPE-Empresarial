using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Llamar carpeta de clases Entities de Comercio.
using SINPE_Empresarial.Domain.SinpeDomain.Entities;

namespace SINPE_Empresarial.Domain.SinpeDomain.Interfaces
{
    public interface SinpeInterface
    {
        // Método: Registra un nuevo sinpe en la base de datos.
        void Registrar(Sinpe sinpe);
    }
}