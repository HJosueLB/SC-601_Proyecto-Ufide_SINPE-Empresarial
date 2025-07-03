using SINPE_Empresarial.Domain.CajaDomain.Entities;
using SINPE_Empresarial.Services.Bitacora.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Services.Bitacora.Mappers
{
    /* 
     * Clase con el objetivo de trasladar la información de los cambios - registros - eliminaciones
     *  hacia el DTO de la entidad para almacenarlos como JSON para la bitacora.
     * 
     * Atributos a almacenar cambios: Entidad Cajas
    */
    public class CajaMapper
    {
        public static CajaDTO ToDTO(Caja caja)
        {
            return new CajaDTO
            {
                IdCaja = caja.IdCaja,
                ComercioId = caja.IdComercio,
                Nombre = caja.Nombre,
                Descripcion = caja.Descripcion,
                TelefonoSINPE = caja.TelefonoSINPE,
                Estado = caja.Estado
            };
        }
    }
}