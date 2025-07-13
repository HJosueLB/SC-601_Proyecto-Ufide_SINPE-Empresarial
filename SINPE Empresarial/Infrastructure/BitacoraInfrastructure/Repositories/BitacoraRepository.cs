using SINPE_Empresarial.Domain.BitacoraDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SINPE_Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE_Empresarial.Infrastructure;


namespace SINPE_Empresarial.Infrastructure.BitacoraInfrastructure.Repositories
{
    public class BitacoraRepository : BitacoraInterface
    {
        public readonly SINPE_Empresarial_DB _context;

        public BitacoraRepository(SINPE_Empresarial_DB context)
        {
            _context = context;
        }

        public async Task RegistrarEvento(BitacoraEvento evento)
        {
            _context.BitacoraEventos.Add(evento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BitacoraEvento>> ObtenerTodosLosEventos()
        {
            return await _context.BitacoraEventos.OrderByDescending(e => e.FechaDeEvento).ToListAsync();
        }
    }
}


