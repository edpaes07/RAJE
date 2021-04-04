using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Admin
{
    public interface IContentsService : IDependencyInjectionService, ICRUDBusinessService<Contents, ContentsResponse, ContentsRequest, ContentsSearchRequest, ContentsSearchResponse>
    {
        Task<IEnumerable<Contents>> GetAll(bool? active);
    }
}
