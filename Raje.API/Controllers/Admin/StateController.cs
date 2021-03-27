using Raje.API.Controllers.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Adm;
using Raje.DL.Services.BLL.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Raje.API.Controllers.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StateController : RajeBaseCRUDController<State, StateResponse, StateRequest, StateSearchRequest, StateResponse>
    {
        public StateController(IStateService stateService) : base(stateService)
        {
        }
    }
}