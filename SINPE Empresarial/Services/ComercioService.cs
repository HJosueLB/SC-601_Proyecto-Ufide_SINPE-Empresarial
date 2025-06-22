using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Llamar carpeta de clases Entities - Interfaces de Comercio.
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Domain.ComercioDomain.Interfaces;

namespace SINPE_Empresarial.Services
{
    public class ComercioService
    {
        // Instancia: Acceso al repositorio de comercio.
        private readonly ComercioInterface _repositorio;

        // Constructor: Inicializa el servicio con acceso al repositorio de comercio.
        public ComercioService(ComercioInterface repositorio)
        {
            _repositorio = repositorio;
        }

        // Método: Listar los comercios existentes en la base de datos.
        public IEnumerable<Comercio> ObtenerTodos()
        {
            return _repositorio.ObtenerTodos();
        }

        // Método: Obtener comercios por ID desde la base de datos.
        public Comercio ObtenerPorId(int id)
        {
            var comercio = _repositorio.ObtenerPorId(id);
            if (comercio == null)
                throw new Exception("Lo sentimos. No se se encontró el comercio.");

            return comercio;
        }

        // Método: Registra un nuevo comercio en la base de datos
        public void Registrar(Comercio comercio)
        {
            if (string.IsNullOrWhiteSpace(comercio.Nombre))
                throw new Exception("El nombre del comercio es obligatorio.");

            // Validación simple de ejemplo
            if (comercio.Nombre.Length > 200)
                throw new Exception("El nombre excede el límite de caracteres establecido.");

            _repositorio.Registrar(comercio);
        }

        // Método: Actualiza los datos de un comercio existente.
        public void Actualizar(Comercio comercio)
        {
            var comercioPorActualizar = _repositorio.ObtenerPorId(comercio.IdComercio);
            if (comercioPorActualizar == null)
                throw new Exception("Lo sentimos. No se pudo encontrar el comercio a actualizar.");


            /* Nota importante: Justificación de utilizar este metodo.
            
            Debido al uso de llaves foráneas  y navegación entre entidades,
            no es recomendable asignar directamente el objeto recibido desde el formulario.
            
            Entity Framework puede tener problemas al hacer tracking del objeto completo,
            como en este caso que las relaciones no estan siendo cargadas o modificadas de forma correcta.
            
            En su lugar, se obtiene el comercio original desde la base de datos y se actualizan solo los atributos
            necesarias una por una. Esto evita errores de referencia y asegura que EF maneje correctamente el estado del objeto.
            */

            // Actualizar Atributos específicas
            comercioPorActualizar.Identificacion = comercio.Identificacion;
            comercioPorActualizar.Nombre = comercio.Nombre;
            comercioPorActualizar.TipoDeComercioId = comercio.TipoDeComercioId;
            comercioPorActualizar.TipoDeIdentificacionId = comercio.TipoDeIdentificacionId;
            comercioPorActualizar.Telefono = comercio.Telefono;
            comercioPorActualizar.CorreoElectronico = comercio.CorreoElectronico;
            comercioPorActualizar.Direccion = comercio.Direccion;
            comercioPorActualizar.Estado = comercio.Estado;
            comercioPorActualizar.FechaDeModificacion = DateTime.Now;

            _repositorio.Actualizar(comercioPorActualizar);
        }


        // Método: Elimina el comercio especificado por ID.
        public void Eliminar(int id)
        {
            var comercio = _repositorio.ObtenerPorId(id);
            if (comercio == null)
                throw new Exception("Lo sentimos. El Comercio no fue encontrado.");

            if (comercio.Estado)
                throw new Exception("Lo sentimos. No se puede eliminar un comercio activo.");

            _repositorio.Eliminar(id);
        }

    }
}