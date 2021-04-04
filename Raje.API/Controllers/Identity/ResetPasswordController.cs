using Raje.DL.Request.Admin.Base;
using Raje.DL.Request.Identity;
using Raje.DL.Services.BLL.Identity;
using Raje.Infra.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raje.API.Controllers.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IResetPasswordService _service;
        public ResetPasswordController(IResetPasswordService service)
        {
            _service = service;
        }

        [HttpPost("Email")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmail([FromForm] ResetPasswordEmailRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.SendEmail(request);
                    return Ok();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
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

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public virtual IActionResult ResetPassword([FromForm] ResetPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.ResetPassword(request);
                    return Ok();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
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

        [HttpPost("UserPassword")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = Policies.OnlyAdminMaster)]
        public virtual IActionResult UserPassword([FromForm] BaseResetPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.ResetUserPassword(request);
                    return Ok();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
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
