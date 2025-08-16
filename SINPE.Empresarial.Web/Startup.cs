using System;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Owin;
using SINPE.Empresarial.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(SINPE.Empresarial.Web.Startup))]
namespace SINPE.Empresarial.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IdentityDb>(IdentityDb.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                SlidingExpiration = true
            });

            SeedRoles.Run();

            // API
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var issuer = ConfigurationManager.AppSettings["Jwt:Issuer"];
            var audience = ConfigurationManager.AppSettings["Jwt:Audience"];
            var keyBase64 = ConfigurationManager.AppSettings["Jwt:KeyBase64"];
            var keyBytes = Convert.FromBase64String(keyBase64);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                }
            });

        }
    }
}

