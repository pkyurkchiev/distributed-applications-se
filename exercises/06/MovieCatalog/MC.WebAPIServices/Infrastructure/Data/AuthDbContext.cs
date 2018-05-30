using Microsoft.AspNet.Identity.EntityFramework;

namespace MC.WebAPIServices.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext()
            : base("AuthDbContext")
        {

        }
    }
}