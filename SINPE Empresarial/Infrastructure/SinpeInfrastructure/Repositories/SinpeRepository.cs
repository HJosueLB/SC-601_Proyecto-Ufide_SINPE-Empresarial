// Llamar carpeta de clases Entities - Interfaces de Comercio.
using SINPE_Empresarial.Domain.SinpeDomain.Entities;
using SINPE_Empresarial.Domain.SinpeDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Infrastructure.SinpeInfrastructure.Repositories
{
    public class SinpeRepository : SinpeInterface
    {
        //Instancia: Acceso a la entidad de Comercio desde la Base de datos.
        private readonly SINPE_Empresarial_DB _context;

        //Constructor: Iniciar contexto de la base de datos.
        public SinpeRepository()
        {
            _context = new SINPE_Empresarial_DB();
        }

        // Método: Registra un nuevo comercio en la base de datos.
        public void Registrar(Sinpe sinpe)
        {
            _context.Sinpe.Add(sinpe);
            _context.SaveChanges();
        }

        // Método: Obtiene todos los sinpes registrados por teléfono de caja.
        public IEnumerable<Sinpe> ObtenerPorTelefonoCaja(string telefonoSINPE)
        {
            return _context.Sinpe
                .Where(s => s.TelefonoDestinatario == telefonoSINPE)
                .OrderByDescending(s => s.FechaDeRegistro)
                .ToList();
        }
    }
}