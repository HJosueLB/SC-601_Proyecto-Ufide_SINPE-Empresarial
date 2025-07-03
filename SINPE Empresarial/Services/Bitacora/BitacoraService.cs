using Newtonsoft.Json;
using SINPE_Empresarial.Domain.BitacoraDomain.Entities;
using SINPE_Empresarial.Domain.BitacoraDomain.Interfaces;
using SINPE_Empresarial.Infrastructure.BitacoraInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SINPE_Empresarial.Services.Bitacora
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