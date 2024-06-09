using Microsoft.AspNetCore.Identity;

namespace firstAsp.Repositories
{
    public interface ITokenRepository
    {
        string CreatJWTToken(IdentityUser user,List<string> roles);

    }
}
