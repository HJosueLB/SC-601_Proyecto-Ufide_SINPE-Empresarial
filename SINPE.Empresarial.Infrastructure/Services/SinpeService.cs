using SINPE.Empresarial.Domain.SinpeDomain.Entities;
using SINPE.Empresarial.Domain.SinpeDomain.Interfaces;

using System.Collections.Generic;

namespace SINPE.Empresarial.Infrastructure.Services
{
    public class SinpeService
    {
        // Instancia: Acceso al repositorio de comercio.
        private readonly SinpeInterface _repositorio;

        public SinpeService(SinpeInterface repositorio)
        {
            _repositorio = repositorio;
        }

        // Método: Registra un nuevo Sinpe en la base de datos
        public void Registrar(Sinpe sinpe)
        {
            _repositorio.Registrar(sinpe);
        }


        public IEnumerable<Sinpe> ObtenerPorTelefonoCaja(string telefonoSINPE)
        {
            return _repositorio.ObtenerPorTelefonoCaja(telefonoSINPE);
        }

    }
}