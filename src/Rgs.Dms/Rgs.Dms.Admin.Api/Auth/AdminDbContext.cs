using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rgs.Dms.Admin.Api.Auth
{
    public class AdminDbContext : IdentityDbContext<IdentityUser>
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
