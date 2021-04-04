using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Base;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Admin
{
    public interface IStateService : IDependencyInjectionService, ICRUDBusinessService<State, StateResponse, StateRequest, StateSearchRequest, StateResponse>
    {
        Task<long> GetIdByAbbreviation(string abbreviation);
    }
}
