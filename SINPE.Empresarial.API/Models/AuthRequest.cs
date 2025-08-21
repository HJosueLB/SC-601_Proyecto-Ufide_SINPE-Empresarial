using System.ComponentModel.DataAnnotations;

namespace SINPE.Empresarial.API.Models
{
    public class AuthRequest
    {
        [Required(ErrorMessage = "El IdComercio es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El IdComercio debe ser mayor que 0")]
        public int IdComercio { get; set; }
    }
}