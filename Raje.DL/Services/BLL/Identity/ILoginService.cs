using Raje.DL.Request.Identity;
using Raje.DL.Response.Identity;
using Raje.DL.Services.BLL.Base;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Identity
{
    public interface IUserNameService : IDependencyInjectionService
    {
        Task<LoginResponse> UserName(LoginRequest request);
        string GetLastGuidAuthentication(string loginName);
        Task<LoginResponse> Refresh(RefreshTokenRequest request);
    }
}
