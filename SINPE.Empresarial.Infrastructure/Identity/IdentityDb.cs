using Microsoft.AspNet.Identity.EntityFramework;

namespace SINPE.Empresarial.Infrastructure.Identity
{
    public class IdentityDb : IdentityDbContext<SINPE.Empresarial.Infrastructure.Identity.ApplicationUser>
    {
        public IdentityDb() : base("name=SINPE_Empresarial_DB") { }

        public static IdentityDb Create() => new IdentityDb();
    }
}
