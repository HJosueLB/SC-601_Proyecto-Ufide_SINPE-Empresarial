using System.ComponentModel.DataAnnotations;

namespace SINPE.Empresarial.Domain.ComercioDomain.Entities
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