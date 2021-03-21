using Raje.Infra.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Raje.API.Controllers.Base
{
    [Authorize(Policy = Policies.UserToken)]
    public abstract class RajeBaseController : ControllerBase
    {
        public RajeBaseController()
        {
        }
    }
}
