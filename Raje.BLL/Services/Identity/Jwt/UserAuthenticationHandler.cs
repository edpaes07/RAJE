using Microsoft.AspNetCore.Authorization;
using Raje.DL.Services.BLL.Identity;
using Raje.Infra.Const;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Identity.Jwt
{
    public class UserAuthenticationHandler : AuthorizationHandler<UserAuthenticationRequirement>
    {
        readonly IUserNameService _service;

        public UserAuthenticationHandler(IUserNameService service)
        {
            _service = service;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthenticationRequirement requirement)
        {
            // Bail out if the office number claim isn't present
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                return Task.CompletedTask;
            }

            // Bail out if we can't read an int from the 'Name' claim
            string loginName = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (String.IsNullOrEmpty(loginName))
            {
                return Task.CompletedTask;
            }

            string lastGuidAuthentication = context.User.FindFirst(c => c.Type == UserClaims.UserLastGuid).Value;
            if (String.IsNullOrEmpty(lastGuidAuthentication))
            {
                return Task.CompletedTask;
            }

            // Finally, validate that the LastGuid value from the claim is equal to LastGuidAuthentication
            string lastUserGuidAuthentication = requirement.GetLastGuidAuthentication(_service, loginName);
            if (lastGuidAuthentication == lastUserGuidAuthentication)
            {
                // Mark the requirement as satisfied
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class UserAuthenticationRequirement : IAuthorizationRequirement
    {
        public UserAuthenticationRequirement()
        {
        }

        public string GetLastGuidAuthentication(IUserNameService _service, string loginName)
        {
            return _service.GetLastGuidAuthentication(loginName);
        }
    }
}
