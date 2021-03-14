using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Admin;
using Microsoft.AspNetCore.Mvc;
using Raje.API.Controllers.Base;
using Raje.DL.Services.BLL.Admin;

namespace Raje.API.Controllers.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseCRUDController<User, UserResponse, UserRequest, UserSearchRequest, UserSearchResponse>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
    }
}
