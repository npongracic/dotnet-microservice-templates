using Microsoft.AspNetCore.Authorization;

namespace SC.API.CleanArchitecture.API.Security
{
    public class TestRequirement : IAuthorizationRequirement
    {
        public TestRequirement()
        {
        }
    }
}
