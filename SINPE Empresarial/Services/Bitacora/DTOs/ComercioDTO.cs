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
     * Atributos a almacenar cambios: Entidad Comercio
    */
    public class ComercioDTO
    {

        public int IdComercio { get; set; }
        public string Identificacion { get; set; }
        public int TipoDeIdentificacionId { get; set; }
        public string Nombre { get; set; }
        public int TipoDeComercioId { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
    }
}