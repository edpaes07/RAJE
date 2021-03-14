using Raje.DL.Request.Base;
using Raje.DL.Response.Identity;
using Raje.DL.Services.BLL.Base;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Identity
{
    public interface ILoginService : IDependencyInjectionService
    {
        Task<LoginResponse> Login(BaseLoginRequest request);

        string GetLastGuidAuthentication(long id);

        Task<LoginResponse> Refresh(BaseRefreshTokenRequest request);
    }
}
