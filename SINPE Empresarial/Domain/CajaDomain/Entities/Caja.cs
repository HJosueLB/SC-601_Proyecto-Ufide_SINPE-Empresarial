using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SINPE_Empresarial.Domain.CajaDomain.Entities
{
    public class Caja
    {
        // Atributo: Llave primaria de la entidad 'Caja'.
        [Key]
        public int IdCaja { get; set; }

        // Atributo: Llave foránea hacia la entidad 'Comercio'.
        [Required]
        public int IdComercio { get; set; }

        // Navegación hacia la entidad 'Comercio' - Llave foránea.
        // Relación de muchos a uno ( Muchas cajas pertencen a un comercio )
        [ForeignKey("IdComercio")]
        public virtual Comercio Comercio { get; set; }

        // Atributo: Nombre de la caja.
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        // Atributo: Descripción de la caja.
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }

        // Atributo: Teléfono SINPE asociado a la caja.
        [Required]
        [MaxLength(10)]
        public string TelefonoSINPE { get; set; }

        // Atributo: Fecha de registro de la caja.
        [Required]
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        // Atributo: Fecha de modificación de la caja.
        public DateTime? FechaDeModificacion { get; set; }

        // Atributo: Estado de la caja (1 – Activo, 0 – Inactivo).
        [Required]
        public bool Estado { get; set; } = true;
    }

}