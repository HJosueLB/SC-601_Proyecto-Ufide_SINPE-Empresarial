using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SINPE.Empresarial.API.Models;
using SINPE.Empresarial.Infrastructure.Data;
using System.Data.Entity;

namespace SINPE.Empresarial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SINPE_Empresarial_DB _db;
        private readonly IConfiguration _configuration;

        public AuthController(SINPE_Empresarial_DB db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        /// <summary>
        /// Genera un token JWT para autenticación del API
        /// </summary>
        /// <param name="authRequest">Solicitud con IdComercio</param>
        /// <returns>Token JWT si el comercio está autorizado</returns>
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] AuthRequest authRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { mensaje = "Datos inválidos", errores = errors });
            }

            try
            {
                // Buscar la configuración para el comercio con el Id dado
                var configuracion = await _db.Configuraciones
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.IdComercio == authRequest.IdComercio && c.Estado == true);

                if (configuracion == null)
                {
                    return Unauthorized(new
                    {
                        mensaje = "No autorizado: Comercio sin configuración registrada o inactiva.",
                        codigo = "COMERCIO_SIN_CONFIGURACION"
                    });
                }

                // Validar que el TipoConfiguracion sea Externa (2) o Ambas (3)
                if (configuracion.TipoConfiguracion != 2 && configuracion.TipoConfiguracion != 3)
                {
                    return Unauthorized(new
                    {
                        mensaje = "No autorizado: El comercio no está configurado para acceso externo.",
                        codigo = "CONFIGURACION_NO_EXTERNA"
                    });
                }

                // Verificar que el comercio existe y está activo
                var comercio = await _db.Comercio
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.IdComercio == authRequest.IdComercio && c.Estado == true);

                if (comercio == null)
                {
                    return Unauthorized(new
                    {
                        mensaje = "No autorizado: Comercio no encontrado o inactivo.",
                        codigo = "COMERCIO_INACTIVO"
                    });
                }

                // Generar el token JWT
                var token = GenerateJwtToken(authRequest.IdComercio, configuracion.TipoConfiguracion);

                return Ok(new AuthResponse
                {
                    Token = token.Token,
                    ExpiresAt = token.ExpiresAt,
                    TokenType = "Bearer"
                });
            }
            catch (Exception ex)
            {
                // Log del error (en un escenario real usarías un logger)
                return StatusCode(500, new
                {
                    mensaje = "Error interno del servidor al generar el token.",
                    codigo = "ERROR_INTERNO"
                });
            }
        }

        private (string Token, DateTime ExpiresAt) GenerateJwtToken(int idComercio, int tipoConfiguracion)
        {
            // Leer parámetros JWT desde la configuración
            var jwtSettings = _configuration.GetSection("JWT");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var secretKey = jwtSettings["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var expiresAt = DateTime.UtcNow.AddHours(1); // Token válido por 1 hora

            // Crear las claims del token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, idComercio.ToString()),
                new Claim("IdComercio", idComercio.ToString()),
                new Claim("TipoConfiguracion", tipoConfiguracion.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // Configurar los parámetros del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return (tokenString, expiresAt);
        }
    }
}
