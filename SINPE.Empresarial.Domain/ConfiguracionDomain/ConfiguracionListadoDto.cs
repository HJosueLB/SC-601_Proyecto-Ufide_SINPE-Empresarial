using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINPE.Empresarial.Domain.ConfiguracionDomain
{
    public class ConfiguracionListadoDto
    {
        public int IdConfiguracion { get; set; }
        public string NombreComercio { get; set; }
        public string TipoConfiguracion { get; set; }
        public int Comision { get; set; }
        public string FechaDeRegistro { get; set; }
        public string FechaDeModificacion { get; set; }
        public string Estado { get; set; }
    }
}