using Raje.DL.Services.BLL.Base;
using Raje.Infra.Const;
using Raje.Infra.Enums;
using Raje.Infra.Util;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Raje.BLL.Services.Base
{
    public abstract class BaseService : IBusinessService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public long UserId
        {
            get
            {
                string userIdValue = _httpContextAccessor.HttpContext.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                long.TryParse(userIdValue, out long userId);
                return userId;
                //throw new KeyNotFoundException("Não foi possivel encontrar dados do usuário atual.");
            }
        }

        public bool IsUserAdmin
        {
            get
            {
                string userRoleValue = _httpContextAccessor.HttpContext.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(userRoleValue))
                    return userRoleValue == UserRoleTypes.AdminMaster.GetDescription();
                return false;
            }
        }

        public bool IsUserGestao
        {
            get
            {
                string userRoleValue = _httpContextAccessor.HttpContext.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(userRoleValue))
                    return userRoleValue == UserRoleTypes.Gestao.GetDescription();
                return false;
            }
        }

        public bool IsUserAdminLoja
        {
            get
            {
                string userRoleValue = _httpContextAccessor.HttpContext.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(userRoleValue))
                    return userRoleValue == UserRoleTypes.AdminLoja.GetDescription();
                return false;
            }
        }

        public bool IsUserOperador
        {
            get
            {
                string userRoleValue = _httpContextAccessor.HttpContext.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(userRoleValue))
                    return userRoleValue == UserRoleTypes.Operador.GetDescription();
                return false;
            }
        }

        public bool IsUserMarketing
        {
            get
            {
                string userRoleValue = _httpContextAccessor.HttpContext.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(userRoleValue))
                    return userRoleValue == UserRoleTypes.Marketing.GetDescription();
                return false;
            }
        }

        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

    }
}
