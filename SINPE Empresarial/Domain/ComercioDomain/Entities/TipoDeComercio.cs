using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Domain.ComercioDomain.Entities
{
    public class TipoDeComercio
    {
        // Atributo: Llave Primaria de la entidad TipoDeComercio.
        [Key]
        public int Id { get; set; }

        // Atributo: Nombre de tipo de comercio.
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
    }
}