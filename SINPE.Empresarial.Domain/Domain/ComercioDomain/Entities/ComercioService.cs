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
            var existente = _repositorio.ObtenerPorId(comercio.IdComercio);
            if (existente == null)
                throw new Exception("Lo sentimos. No se pudo encontrar el comercio a actualizar.");

            comercio.FechaDeModificacion = DateTime.Now;
            _repositorio.Actualizar(comercio);
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