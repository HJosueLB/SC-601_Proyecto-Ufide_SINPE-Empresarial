using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SINPE.Empresarial.Infrastructure.Identity;

namespace SINPE.Empresarial.Web
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
            PasswordValidator = new PasswordValidator { RequiredLength = 6 };
            UserValidator = new UserValidator<ApplicationUser>(this) { RequireUniqueEmail = true };
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
            => new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<IdentityDb>()));
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store) { }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> o, IOwinContext ctx)
            => new ApplicationRoleManager(new RoleStore<IdentityRole>(ctx.Get<IdentityDb>()));
    }

    public static class SeedRoles
    {
        public static void Run()
        {
            using (var ctx = IdentityDb.Create())
            {
                var roleMgr = new ApplicationRoleManager(new RoleStore<IdentityRole>(ctx));
                if (!roleMgr.RoleExists("Administrador")) roleMgr.Create(new IdentityRole("Administrador"));
                if (!roleMgr.RoleExists("Cajero")) roleMgr.Create(new IdentityRole("Cajero"));
            }
        }
    }
}
