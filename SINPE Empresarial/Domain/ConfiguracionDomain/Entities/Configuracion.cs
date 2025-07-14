using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Domain.ConfiguracionDomain.Entities
{
    public class Configuracion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConfiguracion { get; set; }

        [Required]
        [ForeignKey("Comercio")]
        public int IdComercio { get; set; }

        public virtual Comercio Comercio { get; set; }

        [Required]
        public int TipoConfiguracion { get; set; }  // 1=Plataforma, 2=Externa, 3=Ambas

        [Required]
        public int Comision { get; set; }

        [Required]
        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}