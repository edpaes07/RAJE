using Raje.Infra.Enums;
using Raje.Infra.Util;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Identity
{
    public class AtLeastAdminLojaMasterGestorAuthorizationHandler : AuthorizationHandler<AtLeastAdminLojaMasterGestorAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AtLeastAdminLojaMasterGestorAuthorizationRequirement requirement)
        {
            if (context.User.IsInRole(UserRoleTypes.AdminMaster.GetDescription()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class AtLeastAdminLojaMasterGestorAuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
