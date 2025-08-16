using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

// Llamar carpeta de clases Entities - Interfaces de Comercio.
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Domain.ComercioDomain.Interfaces;

namespace SINPE_Empresarial.Infrastructure.ComercioInfrastructure.Repositories
{
    // Implementación: Interfaz de Comercio
    public class ComercioRepository : ComercioInterface
    {
        //Instancia: Acceso a la entidad de Comercio desde la Base de datos.
        private readonly SINPE_Empresarial_DB _context;

        //Constructor: Iniciar contexto de la base de datos.
        public ComercioRepository()
        {
            _context = new SINPE_Empresarial_DB();
        }

        // Método: Listar los comercios existentes en la base de datos.
        public IEnumerable<Comercio> ObtenerTodos()
        {
            return _context.Comercio
                .Include(c => c.TipoDeComercio)
                .Include(c => c.TipoDeIdentificacion)
                .ToList();
        }

        // Método: Obtener comercios por ID desde la base de datos.
        public Comercio ObtenerPorId(int id)
        {
            return _context.Comercio
                .Include(c => c.TipoDeComercio)
                .Include(c => c.TipoDeIdentificacion)
                .FirstOrDefault(c => c.IdComercio == id);
        }

        // Método: Registra un nuevo comercio en la base de datos.
        public void Registrar(Comercio comercio)
        {
            _context.Comercio.Add(comercio);
            _context.SaveChanges();
        }

        // Método: Actualiza los datos de un comercio existente.
        public void Actualizar(Comercio comercio)
        {
            _context.Entry(comercio).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Método: Elimina el comercio especificado por ID.
        public void Eliminar(int id)
        {
            var comercio = _context.Comercio.Find(id);
            if (comercio != null)
            {
                _context.Comercio.Remove(comercio);
                _context.SaveChanges();
            }
        }
    }
}