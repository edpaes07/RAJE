using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Admin;
using Raje.DL.Services.BLL.Base;

namespace Raje.DL.Services.BLL.Admin
{
    public interface IMediaService : IDependencyInjectionService, ICRUDService<Media, MediaResponse, MediaRequest, MediaSearchRequest, MediaSearchResponse>
    {
    }
}
