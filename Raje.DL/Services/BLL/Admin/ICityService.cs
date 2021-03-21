using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Admin
{
    public interface ICityService : IDependencyInjectionService, ICRUDBusinessService<City, CityResponse, CityRequest, CitySearchRequest, CityResponse>
    {
        Task<IEnumerable<CityResponse>> CitiesByStateId(long idState);

        Task<long> GetIdByName(string name);
    }
}
