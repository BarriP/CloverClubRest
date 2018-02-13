using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CloverClubRest.Authorization
{
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type.Equals(ClaimTypes.NameIdentifier) && claim.Value == "admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class AdminRequirement : IAuthorizationRequirement { }
}