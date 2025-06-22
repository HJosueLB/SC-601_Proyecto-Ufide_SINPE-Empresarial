using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SINPE_Empresarial.Domain.ComercioDomain.Entities
{
    public class Comercio
    {
        // Atributo: Llave primaria de la entidad 'Comercio'.
        [Key]
        public int IdComercio { get; set; }

        // Atributo: Identificación - Tamaño 30 (Jurídica 10 digitos - Física 9 digitos).
        [Required]
        [MaxLength(30)]
        public string Identificacion { get; set; }

        // Atributo: Tipo de identificación (Física / Jurídica) - Llave Foranea hacia la entidad 'TipoDeIdentificacion'.
        [Required]
        public int TipoDeIdentificacionId { get; set; }

        // Navegación hacia la entidad 'TipoDeIdentificacion'.
        public virtual TipoDeIdentificacion TipoDeIdentificacion { get; set; }

        // Atributo: Nombre del negocio o persona física.
        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        // Atributo: Tipo de Comercio que desempeña el negocio - Llave Foranea hacia la entidad 'TipoDeComercio'.
        [Required]
        public int TipoDeComercioId { get; set; }

        // Navegación hacia la entidad 'TipoDeComercio'.
        public virtual TipoDeComercio TipoDeComercio { get; set; }

        // Atributo: Número telefónico del negocio.
        [Required]
        [MaxLength(20)]
        public string Telefono { get; set; }

        // Atributo: Correo electrónico del negocio.
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        // Atributo: Dirección de la Ubicación del Negocio.
        [Required]
        [MaxLength(500)]
        public string Direccion { get; set; }

        // Atributo: Fecha para el registro del negocio.
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        // Atributo: Fecha para la modificación de cualquier dato del negocio.
        public DateTime FechaDeModificacion { get; set; } = DateTime.Now;

        // Atributo: Estado actual del negocio (Activo / Inactivo).
        public bool Estado { get; set; } = true;

    }
}