using Raje.API.Controllers.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Raje.API.Controllers.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CityController : RajeBaseCRUDController<City, CityResponse, CityRequest, CitySearchRequest, CityResponse>
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) : base(cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("CitiesByStateId/{idState}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityResponse>>> CitiesByStateId(long idState)
        {
            var retorno = await _cityService.CitiesByStateId(idState);
            if (retorno != null && retorno.Count() > 0)
                return Ok(retorno);
            else
                return Ok(Array.Empty<CityResponse>());
        }

        public override Task<ActionResult<IEnumerable<CityResponse>>> GetAll(bool? active = null)
        {
            throw new NotImplementedException();
        }

    }
}
