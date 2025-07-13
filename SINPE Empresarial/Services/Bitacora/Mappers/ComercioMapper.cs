using SINPE_Empresarial.Domain.ComercioDomain.Entities;
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
     * Atributos a almacenar cambios: Entidad Comercio
    */
    public class ComercioMapper
    {
        public static ComercioDTO ToDTO(Comercio comercio)
        {
            return new ComercioDTO
            {
                IdComercio = comercio.IdComercio,
                Identificacion = comercio.Identificacion,
                TipoDeIdentificacionId = comercio.TipoDeIdentificacionId,
                Nombre = comercio.Nombre,
                TipoDeComercioId = comercio.TipoDeComercioId,
                Telefono = comercio.Telefono,
                CorreoElectronico = comercio.CorreoElectronico,
                Direccion = comercio.Direccion,
                Estado = comercio.Estado
                
            };
        }
    }
}