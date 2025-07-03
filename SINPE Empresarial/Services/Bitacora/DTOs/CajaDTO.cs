using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Services.Bitacora.DTOs
{
    /* 
     * Clase con el objetivo de Almacenar la información de los cambios - registros - eliminaciones
     * para almacenarlos como JSON para la bitacora.
     * 
     * Atributos a almacenar cambios: Entidad Cajas
    */
    public class CajaDTO
    {
        public int IdCaja { get; set; }
        public int ComercioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TelefonoSINPE { get; set; }
        public bool Estado { get; set; }
    }
}