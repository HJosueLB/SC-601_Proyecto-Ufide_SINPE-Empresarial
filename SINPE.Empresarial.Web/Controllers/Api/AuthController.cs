using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Tokens;
using SINPE.Empresarial.Infrastructure.Identity; 

namespace SINPE.Empresarial.Web.Controllers.Api
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        public class LoginDto
        {
            [System.ComponentModel.DataAnnotations.Required]
            [System.ComponentModel.DataAnnotations.EmailAddress]
            public string Email { get; set; }

            [System.ComponentModel.DataAnnotations.Required]
            public string Password { get; set; }
        }

        [HttpPost, AllowAnonymous, Route("token")]
        public async Task<IHttpActionResult> Token(LoginDto dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest("Email y password son requeridos.");

            var userManager = HttpContext.Current
                .GetOwinContext()
                .GetUserManager<ApplicationUserManager>();

            var user = await userManager.FindAsync(dto.Email, dto.Password);
            if (user == null) return Unauthorized();

            var issuer = ConfigurationManager.AppSettings["Jwt:Issuer"];
            var audience = ConfigurationManager.AppSettings["Jwt:Audience"];
            var keyBase64 = ConfigurationManager.AppSettings["Jwt:KeyBase64"];

            if (string.IsNullOrWhiteSpace(issuer) ||
                string.IsNullOrWhiteSpace(audience) ||
                string.IsNullOrWhiteSpace(keyBase64))
            {
                return InternalServerError(new Exception("Faltan AppSettings Jwt:* en web.config."));
            }

            var keyBytes = Convert.FromBase64String(keyBase64);

            var claims = new System.Collections.Generic.List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? string.Empty)
            };

            var roles = await userManager.GetRolesAsync(user.Id);
            foreach (var r in roles) claims.Add(new Claim(ClaimTypes.Role, r));

            var signingKey = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                access_token = jwt,
                token_type = "Bearer",
                expires_in = 3600
            });
        }

        [HttpGet, Authorize, Route("whoami")]
        public IHttpActionResult WhoAmI()
        {
            var id = (ClaimsIdentity)User.Identity;
            var me = new
            {
                name = id.Name,
                email = id.FindFirst(ClaimTypes.Email)?.Value
                        ?? id.FindFirst(JwtRegisteredClaimNames.Email)?.Value,
                roles = id.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray()
            };
            return Ok(me);
        }
    }
}
