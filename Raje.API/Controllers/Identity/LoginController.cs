using Raje.DL.Request.Identity;
using Raje.DL.Response.Identity;
using Raje.DL.Services.BLL.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Raje.API.Controllers.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IUserNameService _service;
        public LoginController(IUserNameService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<LoginResponse>> Post([FromForm] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginResult = await _service.UserName(request);
                    return Ok(loginResult);
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

        [HttpPost("RefreshToken")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<LoginResponse>> RefreshToken([FromForm] RefreshTokenRequest request)
        {
            var loginResult = await _service.Refresh(request);
            return Ok(loginResult);
        }
    }
}
