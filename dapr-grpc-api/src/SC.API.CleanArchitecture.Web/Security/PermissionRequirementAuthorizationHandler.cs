using SC.API.CleanArchitecture.Application.Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Security
{
    //public class StatementLifeCyclePermissionRequirementAuthorizationHandler : AuthorizationHandler<PermissionRequirement, List<StatementLifeCycleEnum>>
    //{
    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement, List<StatementLifeCycleEnum> resource)
    //    {
    //        var permissionClaimValue = context.User.FindFirstValue(AppClaimTypes.Permission);

    //        byte[] flagsbytesOut = Convert.FromBase64String(permissionClaimValue);
    //        BitArray permissions = new BitArray(flagsbytesOut);

    //        //!requirement.Permissions.Except(permissions).Any();

    //        var checkPermissions = new List<PermissionEnum>();

    //        foreach (var lifeCycle in resource) {
    //            checkPermissions.Add((PermissionEnum)Enum.Parse(typeof(PermissionEnum), "StatementLifeCycle" + lifeCycle.ToString()));
    //        }
            
    //        if (checkPermissions.All(c => permissions.Get((int)c))) {
    //            context.Succeed(requirement);
    //        }
    //        else {
    //            context.Fail();
    //        }

    //        return Task.CompletedTask;
    //    }
    //}

    public class OrganizationPermissionRequirementAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionClaimValue = context.User.FindFirstValue(AppClaimTypes.Permissions);

            if (permissionClaimValue == null) {
                return Task.CompletedTask;
            }

            byte[] flagsbytesOut = Convert.FromBase64String(permissionClaimValue);
            BitArray permissions = new BitArray(flagsbytesOut);

            //!requirement.Permissions.Except(permissions).Any();

            if (requirement.OrganizationPermissions.Length > 0 && requirement.OrganizationPermissions.All(c => permissions.Get((int) c)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class ServiceProviderPermissionRequirementAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionClaimValue = context.User.FindFirstValue(AppClaimTypes.Permissions);

            byte[] flagsbytesOut = Convert.FromBase64String(permissionClaimValue);
            BitArray permissions = new BitArray(flagsbytesOut);

            if (requirement.ServiceProviderPermissions.Length > 0 && requirement.ServiceProviderPermissions.All(c => permissions.Get((int)c)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class PlatformOwnerPermissionRequirementAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissionClaimValue = context.User.FindFirstValue(AppClaimTypes.Permissions);

            byte[] flagsbytesOut = Convert.FromBase64String(permissionClaimValue);
            BitArray permissions = new BitArray(flagsbytesOut);

            if (requirement.PlatformOwnerPermissions.Length > 0 && requirement.PlatformOwnerPermissions.All(c => permissions.Get((int)c)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
