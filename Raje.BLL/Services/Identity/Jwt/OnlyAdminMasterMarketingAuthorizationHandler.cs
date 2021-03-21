using Raje.Infra.Enums;
using Raje.Infra.Util;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Identity
{
    public class OnlyAdminMasterMarketingAuthorizationHandler : AuthorizationHandler<OnlyAdminMasterMarketingAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyAdminMasterMarketingAuthorizationRequirement requirement)
        {
            if (context.User.IsInRole(UserRoleTypes.AdminMaster.GetDescription()) || 
                context.User.IsInRole(UserRoleTypes.Marketing.GetDescription()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class OnlyAdminMasterMarketingAuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
                    