using Raje.API.Controllers.Base;
using Raje.DL.DB.Admin;
using Raje.DL.Request.Admin;
using Raje.DL.Response.Admin;
using Raje.DL.Services.BLL.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Raje.API.Controllers.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MediaController : BaseCRUDController<Media, MediaResponse, MediaRequest, MediaSearchRequest, MediaSearchResponse>
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService service) : base(service)
        {
            _mediaService = service;
        }
    }
}
