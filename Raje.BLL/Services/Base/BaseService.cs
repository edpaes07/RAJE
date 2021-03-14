using Raje.DL.Services.BLL.Base;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Raje.BLL.Services.Base
{
    public abstract class BaseService : IBaseService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public long UserId
        {
            get
            {
                string userIdValue = _httpContextAccessor.HttpContext.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                long.TryParse(userIdValue, out long userId);
                return userId;
            }
        }

        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

    }
}
