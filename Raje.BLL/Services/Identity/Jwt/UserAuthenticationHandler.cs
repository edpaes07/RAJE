using Raje.DL.Services.BLL.Identity;
using Raje.Infra.Const;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Identity.Jwt
{
    public class UserAuthenticationHandler : AuthorizationHandler<UserAuthenticationRequirement>
    {
        readonly ILoginService _service;

        public UserAuthenticationHandler(ILoginService service)
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
            string id = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (String.IsNullOrEmpty(id))
            {
                return Task.CompletedTask;
            }

            string lastGuidAuthentication = context.User.FindFirst(c => c.Type == UserClaims.UserLastGuid).Value;
            if (String.IsNullOrEmpty(lastGuidAuthentication))
            {
                return Task.CompletedTask;
            }

            // Finally, validate that the LastGuid value from the claim is equal to LastGuidAuthentication
            string lastUserGuidAuthentication = requirement.GetLastGuidAuthentication(_service, Convert.ToInt64(id));
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

        public string GetLastGuidAuthentication(ILoginService _service, long id)
        {
            return _service.GetLastGuidAuthentication(id);
        }
    }
}