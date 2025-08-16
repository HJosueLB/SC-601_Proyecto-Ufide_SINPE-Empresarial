using System.ComponentModel.DataAnnotations;

namespace SINPE.Empresarial.Domain.ComercioDomain.Entities
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