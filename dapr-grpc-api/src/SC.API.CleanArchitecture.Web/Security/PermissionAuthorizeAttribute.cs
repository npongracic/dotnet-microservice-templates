using Microsoft.AspNetCore.Authorization;

namespace SC.API.CleanArchitecture.API.Security
{
    public sealed class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizeData
    {
        public PermissionAuthorizeAttribute() : base() { }
        public PermissionAuthorizeAttribute(string policy) : base(policy) { }
        public PermissionAuthorizeAttribute(PermissionEnum permission)
        {
            Policy = permission + "Policy";
        }
    }
}
