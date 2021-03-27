using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Base;

namespace Raje.DL.Services.BLL.Admin
{
    public interface IUserService : IDependencyInjectionService, ICRUDBusinessService<User, UserResponse, UserRequest, UserSearchRequest, UserSearchResponse>
    {
    }
}
