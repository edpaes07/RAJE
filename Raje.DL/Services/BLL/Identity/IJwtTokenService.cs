using Raje.DL.DB.Admin;
using Raje.DL.Services.BLL.Base;

namespace Raje.DL.Services.BLL.Identity
{
    public interface IJwtTokenService : IDependencyInjectionService
    {
        string GenerateJwtToken(User model);
    }
}
