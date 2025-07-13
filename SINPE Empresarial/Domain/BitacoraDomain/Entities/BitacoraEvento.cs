using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Domain.BitacoraDomain.Entities
{
    public class BitacoraEvento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEvento { get; set; }

        [Required]
        [StringLength(20)]
        public string TablaDeEvento { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoDeEvento { get; set; }

        [Required]
        public DateTime FechaDeEvento { get; set; } = DateTime.Now;

        [Required]
        public string DescripcionDeEvento { get; set; }

        public string StackTrace { get; set; }

        public string DatosAnteriores { get; set; }

        public string DatosPosteriores { get; set; }
    }

    // Constantes para los eventos a registrar en la bitácora
    public static class TiposDeEvento
    {
        public const string Registrar = "Registrar";
        public const string Editar = "Editar";
        public const string Eliminar = "Eliminar";
        public const string Error = "Error";
    }
}
