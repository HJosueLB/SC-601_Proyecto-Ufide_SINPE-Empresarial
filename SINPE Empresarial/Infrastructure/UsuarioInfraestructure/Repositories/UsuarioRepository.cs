using SINPE_Empresarial.Domain.UsuarioDomain.Entities;
using SINPE_Empresarial.Domain.UsuarioDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SINPE_Empresarial.Infrastructure.UsuarioInfrastructure.Repositories
{
    // Implementación: Interfaz de Usuario
    public class UsuarioRepository : UsuarioInterface
    {
        // Instancia: contexto de la base de datos
        private readonly SINPE_Empresarial_DB _context;

        // Constructor: inicializa el contexto
        public UsuarioRepository()
        {
            _context = new SINPE_Empresarial_DB();
        }

        // Método: Obtener todos los usuarios
        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _context.Usuarios
                .Include(u => u.Comercio)
                .ToList();
        }

        // Método: Obtener un usuario por ID
        public Usuario ObtenerPorId(int id)
        {
            return _context.Usuarios
                .Include(u => u.Comercio)
                .FirstOrDefault(u => u.IdUsuario == id);
        }

        // Método: Registrar nuevo usuario
        public void Registrar(Usuario usuario)
        {
            usuario.FechaDeRegistro = DateTime.Now;
            usuario.Estado = true;

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        // Método: Actualizar usuario existente
        public void Editar(Usuario usuario)
        {
            var existente = _context.Usuarios.Find(usuario.IdUsuario);

            if (existente != null)
            {
                existente.Nombres = usuario.Nombres;
                existente.PrimerApellido = usuario.PrimerApellido;
                existente.SegundoApellido = usuario.SegundoApellido;
                existente.Identificacion = usuario.Identificacion;
                existente.CorreoElectronico = usuario.CorreoElectronico;
                existente.Estado = usuario.Estado;
                existente.FechaDeModificacion = DateTime.Now;

                _context.Entry(existente).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
