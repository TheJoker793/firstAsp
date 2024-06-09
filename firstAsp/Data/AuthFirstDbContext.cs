using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace firstAsp.Data
{
    public class AuthFirstDbContext:IdentityDbContext
    {
        public AuthFirstDbContext(DbContextOptions<AuthFirstDbContext> dbContextOptions):base(dbContextOptions) 
        {

            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "6157f8e0-f826-49b2-96de-65ab012c6517";
            var writerRoleId = "9e9cdd2d-b6a0-446e-8518-5841d3db5b45";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
