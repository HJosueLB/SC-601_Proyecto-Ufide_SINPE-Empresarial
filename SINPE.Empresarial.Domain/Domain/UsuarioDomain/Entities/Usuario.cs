using SINPE.Empresarial.Domain.ComercioDomain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SINPE.Empresarial.Domain.UsuarioDomain.Entities
{
    public class Usuario
    {
        // Atributo: Llave primaria de la entidad 'Usuario'.
        [Key]
        public int IdUsuario { get; set; }

        // Atributo: Llave foránea hacia la entidad 'Comercio'.
        [Required]
        public int IdComercio { get; set; }

        // Navegación hacia la entidad 'Comercio'.
        [ForeignKey("IdComercio")]
        public virtual Comercio Comercio { get; set; }

        // Atributo: ID de autenticación (opcional).
        public Guid? IdNetUser { get; set; }

        // Atributo: Nombres del usuario.
        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }

        // Atributo: Primer apellido del usuario.
        [Required]
        [MaxLength(100)]
        public string PrimerApellido { get; set; }

        // Atributo: Segundo apellido del usuario.
        [Required]
        [MaxLength(100)]
        public string SegundoApellido { get; set; }

        // Atributo: Identificación del usuario.
        [Required]
        [MaxLength(10)]
        public string Identificacion { get; set; }

        // Atributo: Correo electrónico del usuario.
        [Required]
        [MaxLength(200)]
        public string CorreoElectronico { get; set; }

        // Atributo: Fecha de registro del usuario.
        [Required]
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        // Atributo: Fecha de modificación del usuario.
        public DateTime? FechaDeModificacion { get; set; }

        // Atributo: Estado del usuario (1 – Activo, 0 – Inactivo).
        [Required]
        public bool Estado { get; set; }


    }
}
