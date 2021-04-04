using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Base;
using Raje.DL.Services.DAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raje.DL.Services.DAL.QueryServices
{
    public interface ILogQueryService : IDependencyInjectionService
    {
        Task<IEnumerable<string>> GetAllDistinctAPIs();

    }
}
