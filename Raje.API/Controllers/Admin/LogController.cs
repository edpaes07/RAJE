using Raje.API.Controllers.Base;
using Raje.DL.Request.Admin;
using Raje.DL.Request.Admin.Base;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Raje.Infra.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raje.Infra.Util;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Raje.API.Controllers.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class LogController : RajeBaseController
    {
        public readonly ILogService _service;

        public LogController(ILogService service) : base()
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policies.OnlyAdminMaster)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public virtual ActionResult<LogResponse> Get(long id)
        {
            try
            {
                var TResponse = _service.GetById(id);
                return Ok(TResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllDistinctAPIs")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<IEnumerable<string>>> GetAllDistinctAPIs()
        {
            try
            {
                var response = await _service.GetAllDistinctAPIs();
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("Export")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Produces("text/csv")]
        [Authorize(Policy = Policies.OnlyAdminMaster)]
        public async Task<ActionResult<string>> ExportLogs(LogSearchRequest search)
        {
            try
            {
                var searchResult = await _service.SearchLogs(search);
            var content = Encoding.UTF8.GetBytes(CSVHelper<LogReportResponse>.WriteContent(searchResult));
            return File(content, "text/csv");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
