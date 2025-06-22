using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Domain.ComercioDomain.Entities
{
    public class TipoDeIdentificacion
    {
        // Atributo: Llave Primaria de la entidad TipoDeIdentificacion.
        [Key]
        public int Id { get; set; }

        // Atributo: Nombre de tipo de identificación
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}