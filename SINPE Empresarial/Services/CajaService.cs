using SINPE_Empresarial.Domain.CajaDomain.Entities;
using SINPE_Empresarial.Domain.CajaDomain.Intefaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SINPE_Empresarial.Services
{
    public class CajaService
    {
        // Instancia: Acceso al repositorio de caja.
        private readonly CajaInterface _cajaRepository;

        // Constructor: Inicializa el servicio con acceso al repositorio de caja.
        public CajaService(CajaInterface cajaRepository)
        {
            _cajaRepository = cajaRepository;
        }

        // Método: Listar las cajas asociadas a un comercio seleccionado.
        public IEnumerable<Caja> ObtenerCajasPorComercio(int idComercio)
        {
            return _cajaRepository.ObtenerPorComercio(idComercio);
        }

        // Método: Obtener una caja específica por ID.
        public Caja ObtenerPorId(int id)
        {
            return _cajaRepository.ObtenerPorId(id);
        }

        // Método: Validar si ya existe una caja con el mismo nombre en el comercio.
        public void Registrar(Caja caja)
        {
            if (_cajaRepository.ExisteNombreEnComercio(caja.Nombre, caja.IdComercio))
                throw new InvalidOperationException("Importante, La caja a registrar ya existe en este comercio.");

            if (_cajaRepository.ExisteTelefonoActivo(caja.TelefonoSINPE))
                throw new InvalidOperationException("Importante, El teléfono SINPE ya existe en una caja activa.");

            _cajaRepository.Registrar(caja);
        }

        // Método: Actualizar los datos de una caja existente.
        public void Actualizar(Caja caja)
        {
            if (_cajaRepository.ExisteNombreEnComercio(caja.Nombre, caja.IdComercio, caja.IdCaja))
                throw new InvalidOperationException("Importante, La caja a registrar ya existe en este comercio.");

            if (_cajaRepository.ExisteTelefonoActivo(caja.TelefonoSINPE, caja.IdCaja))
                throw new InvalidOperationException("Importante, El teléfono SINPE ya existe en una caja activa.");

            _cajaRepository.Actualizar(caja);
        }
    }
}
