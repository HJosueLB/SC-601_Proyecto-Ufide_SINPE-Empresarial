using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Domain.SinpeDomain.Entities
{
    public class Sinpe
    {
        // Atributo: Llave primaria de la entidad 'Sinpe'.
        [Key]
        public int IdSinpe { get; set; }

        // Atributo: Teléfono desde el cual se realiza el pago.
        [Required]
        [MaxLength(10)]
        public string TelefonoOrigen { get; set; }

        // Atributo: Nombre de quien realiza el pago.
        [Required]
        [MaxLength(200)]
        public string NombreOrigen { get; set; }

        // Atributo: Teléfono de la caja destino.
        [Required]
        [MaxLength(10)]
        public string TelefonoDestinatario { get; set; }

        // Atributo: Nombre de la caja destino.
        [Required]
        [MaxLength(200)]
        public string NombreDestinatario { get; set; }

        // Atributo: Monto del pago.
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        // Atributo: Fecha de registro del pago (automática).
        [Required]
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        // Atributo: Descripción del pago (opcional).
        [MaxLength(50)]
        public string Descripcion { get; set; }

        // Atributo: Estado de sincronización (1 – Sincronizado, 0 – No sincronizado).
        [Required]
        public bool Estado { get; set; }
    }
}