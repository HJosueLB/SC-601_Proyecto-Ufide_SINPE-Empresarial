using SINPE.Empresarial.Domain.BitacoraDomain.Entities;
using SINPE.Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE.Empresarial.Infrastructure.Data;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SINPE.Empresarial.Infrastructure.Repositories
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


