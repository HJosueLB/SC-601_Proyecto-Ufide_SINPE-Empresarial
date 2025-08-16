using SINPE.Empresarial.Domain.BitacoraDomain.Entities;
using SINPE.Empresarial.Domain.BitacoraDomain.Interfaces;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SINPE.Empresarial.Infrastructure.Services
{
    public class BitacoraService : IBitacoraManager
    {
        public readonly BitacoraInterface _bitacoraRepository;

        public BitacoraService(BitacoraInterface bitacoraRepository)
        {
            _bitacoraRepository = bitacoraRepository;
        }
        public async Task RegistrarEvento(
            string tablaDeEvento,
            string tipoDeEvento,
            string descripcionDeEvento,
            object datosAnteriores = null,
            object datosPosteriores = null,
            string stackTrace = null)
        {
            var evento = new BitacoraEvento
            {
                TablaDeEvento = tablaDeEvento,
                TipoDeEvento = tipoDeEvento,
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = descripcionDeEvento,
                StackTrace = stackTrace,
                DatosAnteriores = datosAnteriores != null ? JsonConvert.SerializeObject(datosAnteriores) : null,
                DatosPosteriores = datosPosteriores != null ? JsonConvert.SerializeObject(datosPosteriores) : null
            };


            await _bitacoraRepository.RegistrarEvento(evento);
        }

        public async Task<IEnumerable<BitacoraEvento>> ObtenerEventos()
        {
            return await _bitacoraRepository.ObtenerTodosLosEventos();
        }
    }
}