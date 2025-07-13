using SINPE_Empresarial.Domain.CajaDomain.Entities;
using SINPE_Empresarial.Domain.CajaDomain.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SINPE_Empresarial.Infrastructure.CajaInfrastructure.Repositories
{
    // Implementación: Interfaz de Cajas
    public class CajaRepository : CajaInterface
    {
        //Instancia: Acceso a la entidad de Comercio desde la Base de datos.
        private readonly SINPE_Empresarial_DB _context;

        //Constructor: Iniciar contexto de la base de datos.
        public CajaRepository()
        {
            _context = new SINPE_Empresarial_DB();
        }

        // Método: Listar las cajas existentes en la base de datos acorde al comercio seleccionado.
        public IEnumerable<Caja> ObtenerPorComercio(int idComercio)
        {
            return _context.Cajas
                .Where(c => c.IdComercio == idComercio)
                .ToList();
        }

        // Método: Obtener una caja por ID desde la base de datos.
        public Caja ObtenerPorId(int id)
        {
            return _context.Cajas
                .Include(c => c.Comercio)
                .FirstOrDefault(c => c.IdCaja == id);
        }

        // Método: Registrar una nueva caja, esta se asigna al comercio donde se encuentra.
        public void Registrar(Caja caja)
        {
            caja.FechaDeRegistro = DateTime.Now;
            caja.Estado = true;

            _context.Cajas.Add(caja);
            _context.SaveChanges();
        }

        // Método: Actualizar los datos de una caja existente.
        public void Actualizar(Caja caja)
        {
            var existente = _context.Cajas.Find(caja.IdCaja);
            if (existente == null)
                throw new Exception("Caja no encontrada");

            // Actualizar los campos
            existente.Nombre = caja.Nombre;
            existente.Descripcion = caja.Descripcion;
            existente.TelefonoSINPE = caja.TelefonoSINPE;
            existente.Estado = caja.Estado;
            existente.FechaDeModificacion = DateTime.Now;

            _context.SaveChanges();
        }

        // Método: Validar si ya existe una caja con el mismo nombre en el comercio.
        public bool ExisteNombreEnComercio(string nombre, int idComercio, int? idCaja = null)
        {
            return _context.Cajas.Any(c =>
                c.Nombre == nombre &&
                c.IdComercio == idComercio &&
                (!idCaja.HasValue || c.IdCaja != idCaja.Value));
        }

        // Método: Validar si existe una caja activa con el mismo número de teléfono SINPE.
        public bool ExisteTelefonoActivo(string telefonoSINPE, int? idCaja = null)
        {
            return _context.Cajas.Any(c =>
                c.TelefonoSINPE == telefonoSINPE &&
                c.Estado == true &&
                (!idCaja.HasValue || c.IdCaja != idCaja.Value));
        }
    }
}
