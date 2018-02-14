using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CloverClubRest.Authorization
{
    public class UserHandler : AuthorizationHandler<UserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            if(!context.User.HasClaim(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)))
            {
                return Task.CompletedTask;
            }

            if (!context.User.HasClaim(claim => claim.Type.Equals(ClaimTypes.NameIdentifier) && claim.Value.Equals("admin")))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class UserRequirement : IAuthorizationRequirement {}
}
