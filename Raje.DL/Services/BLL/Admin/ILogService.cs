using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raje.DL.Services.BLL.Admin
{
    public interface ILogService : IDependencyInjectionService
    {
        LogResponse GetById(long id);

        long Create(LogRequest model);

        Task<IEnumerable<string>> GetAllDistinctAPIs();

        Task<IEnumerable<LogReportResponse>> SearchLogs(LogSearchRequest search);
    }
}
