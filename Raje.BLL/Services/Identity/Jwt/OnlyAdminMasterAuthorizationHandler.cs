using Raje.Infra.Enums;
using Raje.Infra.Util;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Identity
{
    public class OnlyAdminMasterAuthorizationHandler : AuthorizationHandler<OnlyAdminMasterAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyAdminMasterAuthorizationRequirement requirement)
        {
            if (context.User.IsInRole(UserRoleTypes.AdminMaster.GetDescription()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class OnlyAdminMasterAuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
