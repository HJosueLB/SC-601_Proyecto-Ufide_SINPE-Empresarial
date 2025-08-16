using SINPE.Empresarial.Domain.ComercioDomain.Entities;
using SINPE.Empresarial.Domain.UsuarioDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINPE.Empresarial.Domain.UsuarioDomain.Interfaces
{
    public interface UsuarioInterface
    {
        void Registrar(Usuario usuario);
        IEnumerable<Usuario> ObtenerTodos();

        Usuario ObtenerPorId(int id);

        void Editar(Usuario usuario);
    }
}