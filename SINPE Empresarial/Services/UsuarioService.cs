using SINPE_Empresarial.Domain.UsuarioDomain.Entities;
using SINPE_Empresarial.Domain.UsuarioDomain.Interfaces;
using System.Collections.Generic;

namespace SINPE_Empresarial.Services
{
    public class UsuarioService
    {
        private readonly UsuarioInterface _usuarioRepo;

        // Constructor
        public UsuarioService(UsuarioInterface usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        // Obtener todos los usuarios
        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _usuarioRepo.ObtenerTodos();
        }

        // Obtener un usuario por ID
        public Usuario ObtenerPorId(int id)
        {
            return _usuarioRepo.ObtenerPorId(id);
        }

        // Registrar un nuevo usuario
        public void Registrar(Usuario usuario)
        {
            _usuarioRepo.Registrar(usuario);
        }

        // Actualizar un usuario existente
        public void Actualizar(Usuario usuario)
        {
            _usuarioRepo.Editar(usuario);
        }
    }
}
