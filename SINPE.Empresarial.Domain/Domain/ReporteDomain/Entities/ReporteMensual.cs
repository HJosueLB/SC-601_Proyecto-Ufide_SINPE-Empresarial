using SINPE.Empresarial.Domain.ComercioDomain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SINPE.Empresarial.Domain.ReporteDomain.Entities
{
    public class ReporteMensual
    {

        // Atributo: Id Reporte
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReporte { get; set; }

        // Atributo: Id Comercio
        [Required]
        [ForeignKey("Comercio")]
        public int IdComercio { get; set; }
        public virtual Comercio Comercio { get; set; }

        // Atributo: Cantidad de cajas
        [Required]
        public int CantidadDeCajas { get; set; }

        // Atributo: Monto total recaudado
        [Required]
        public decimal MontoTotalRecaudado { get; set; }

        // Atributo: Cantidad de SINPES procesados
        [Required]
        public int CantidadDeSINPES { get; set; }

        // Atributo: Monto total de comisiones
        [Required]
        public decimal MontoTotalComision { get; set; }

        // Atributo: Fecha del reporte
        [Required]
        public DateTime FechaDelReporte { get; set; }
    }
}