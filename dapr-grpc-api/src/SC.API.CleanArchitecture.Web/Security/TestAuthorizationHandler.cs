using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Security
{
    public class TestAuthorizationHandler : AuthorizationHandler<TestRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestRequirement requirement)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
