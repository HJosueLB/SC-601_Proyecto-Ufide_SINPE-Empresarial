// Services/CajaService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Entities;
using YourProject.Domain.Interfaces;
using YourProject.Infrastructure;

namespace YourProject.Services
{
    public class CajaService : ICajaService
    {
        private readonly ApplicationDbContext _context;

        public CajaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Caja> ObtenerCajasPorComercio(int idComercio)
        {
            return _context.Cajas.Where(c => c.IdComercio == idComercio).ToList();
        }

        public Caja ObtenerCaja(int id)
        {
            return _context.Cajas.Find(id);
        }

        public void CrearCaja(Caja caja)
        {
            // Validaciones
            if (_context.Cajas.Any(c => c.Nombre == caja.Nombre && c.IdComercio == caja.IdComercio))
            {
                throw new Exception("Ya existe una caja con el mismo nombre para este comercio.");
            }

            if (_context.Cajas.Any(c => c.TelefonoSINPE == caja.TelefonoSINPE && c.Estado))
            {
                throw new Exception("Ya existe una caja activa con el mismo teléfono SINPE.");
            }

            caja.FechaDeRegistro = DateTime.Now;
            caja.Estado = true; // Activo por defecto
            _context.Cajas.Add(caja);
            _context.SaveChanges();
        }

        public void EditarCaja(Caja caja)
        {
            var cajaExistente = _context.Cajas.Find(caja.IdCaja);
            if (cajaExistente == null)
            {
                throw new Exception("Caja no encontrada.");
            }

            // Validaciones
            if (_context.Cajas.Any(c => c.Nombre == caja.Nombre && c.IdComercio == caja.IdComercio && c.IdCaja != caja.IdCaja))
            {
                throw new Exception("Ya existe una caja con el mismo nombre para este comercio.");
            }

            if (_context.Cajas.Any(c => c.TelefonoSINPE == caja.TelefonoSINPE && c.Estado && c.IdCaja != caja.IdCaja))
            {
                throw new Exception("Ya existe una caja activa con el mismo teléfono SINPE.");
            }

            cajaExistente.Nombre = caja.Nombre;
            cajaExistente.Descripcion = caja.Descripcion;
            cajaExistente.TelefonoSINPE = caja.TelefonoSINPE;
            cajaExistente.FechaDeModificacion = DateTime.Now;
            cajaExistente.Estado = caja.Estado;

            _context.SaveChanges();
        }

        public void EliminarCaja(int id)
        {
            var caja = _context.Cajas.Find(id);
            if (caja != null)
            {
                _context.Cajas.Remove(caja);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Sinpe> ObtenerSinpePorCaja(int idCaja)
        {
            return _context.Sinpes.Where(s => s.IdCaja == idCaja).OrderByDescending(s => s.Fecha).ToList();
        }
    }
}