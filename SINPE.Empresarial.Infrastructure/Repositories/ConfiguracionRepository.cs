using SINPE.Empresarial.Domain.ConfiguracionDomain.Entities;
using SINPE.Empresarial.Domain.ConfiguracionDomain.Interfaces;
using SINPE.Empresarial.Domain.ConfiguracionDomain;
using SINPE.Empresarial.Infrastructure.Data;

using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SINPE.Empresarial.Infrastructure.Repositories

{
    public class ConfiguracionRepository : ConfiguracionInterface
    {
        private readonly SINPE_Empresarial_DB _context;

        public ConfiguracionRepository()
        {
            _context = new SINPE_Empresarial_DB();
        }

        public IEnumerable<Configuracion> ObtenerTodos()
        {
            return _context.Configuraciones.ToList();

        }

        public void Agregar(Configuracion configuracion)
        {
            _context.Configuraciones.Add(configuracion);
            _context.SaveChanges();
        }

        public void Actualizar(Configuracion configuracion)
        {
            _context.Entry(configuracion).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<ConfiguracionListadoDto> ListarConfiguraciones()
        {
            var lista = _context.Configuraciones
        .Select(c => new
        {
            c.IdConfiguracion,
            NombreComercio = c.Comercio.Nombre,
            c.TipoConfiguracion,
            c.Comision,
            c.FechaDeRegistro,
            c.FechaDeModificacion,
            c.Estado
        })
        .ToList()  // Aquí termina la ejecución en la base de datos
        .Select(c => new ConfiguracionListadoDto
        {
            IdConfiguracion = c.IdConfiguracion,
            NombreComercio = c.NombreComercio,
            TipoConfiguracion = c.TipoConfiguracion == 1 ? "Plataforma" :
                                c.TipoConfiguracion == 2 ? "Externa" :
                                "Ambas",
            Comision = c.Comision,
            FechaDeRegistro = c.FechaDeRegistro.ToString("yyyy-MM-dd HH:mm:ss"),
            FechaDeModificacion = c.FechaDeModificacion.HasValue
                                  ? c.FechaDeModificacion.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                  : "Sin modificar",
            Estado = c.Estado ? "Activo" : "Inactivo"
        });

            return lista;
        }

        public Configuracion ObtenerPorId(int id)
        {
            return _context.Configuraciones
                           .Include(c => c.Comercio)  
                           .FirstOrDefault(c => c.IdConfiguracion == id);
        }

        public bool ExisteConfiguracionPorComercio(int idComercio)
        {
            return _context.Configuraciones.Any(c => c.IdComercio == idComercio);
        }

    }
}