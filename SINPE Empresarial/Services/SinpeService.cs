// Llamar carpeta de clases Entities - Interfaces de Sinpe.
using SINPE_Empresarial.Domain.SinpeDomain.Entities;
using SINPE_Empresarial.Domain.SinpeDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Services
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
    }
}