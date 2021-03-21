using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;


namespace Raje.DL.Services.BLL.Admin
{
    public interface ILoggingMiddlewareService
    {
        Task Invoke(HttpContext context, ILogService _service);

        public Task<LogMiddlewareRequest> FormatRequest(HttpRequest request);

        public LogMiddlewareResponse FormatResponse(HttpResponse response);

        byte[] ReadFully(Stream input);
    }
}
