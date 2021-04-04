using Raje.API.Controllers.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Raje.Infra.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using AutoMapper;

namespace Raje.API.Controllers.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = Policies.UserToken)]
    public class ContentsController : RajeBaseCRUDController<Contents, ContentsResponse, ContentsRequest, ContentsSearchRequest, ContentsSearchResponse>
    {
        public readonly IContentsService _service;
        public readonly IMapper _mapper;

        public ContentsController(IContentsService service, IMapper mapper) : base(service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult<IEnumerable<ContentsResponse>>> GetAll(bool? active = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<Contents> contents = await _service.GetAll(active);

                    return Ok(_mapper.Map<List<ContentsResponse>>(contents));
                }
                catch (ArgumentNullException ex)
                {
                    return BadRequest(ex.ParamName);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
