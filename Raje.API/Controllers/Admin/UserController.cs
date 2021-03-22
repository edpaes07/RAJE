using Raje.API.Controllers.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Raje.Infra.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Raje.API.Controllers.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = Policies.UserToken)]
    public class UserController : RajeBaseCRUDController<User, UserResponse, UserRequest, UserSearchRequest, UserSearchResponse>
    {
        public readonly IUserService _service;

        public UserController(IUserService service) : base(service)
        {
            _service = service;
        }
    }
}
