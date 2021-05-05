using Microsoft.AspNetCore.Authorization;
using System;

namespace SC.API.CleanArchitecture.API.Security
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(PermissionEnum[] organizationPermissions = null, PermissionEnum[] serviceProviderPermissions = null, PermissionEnum[] platformOwnerPermissions = null)
        {
            OrganizationPermissions = organizationPermissions ?? Array.Empty<PermissionEnum>();
            ServiceProviderPermissions = serviceProviderPermissions ?? Array.Empty<PermissionEnum>();
            PlatformOwnerPermissions = platformOwnerPermissions ?? Array.Empty<PermissionEnum>();
        }

        public PermissionEnum[] OrganizationPermissions { get; set; }
        public PermissionEnum[] ServiceProviderPermissions { get; set; }
        public PermissionEnum[] PlatformOwnerPermissions { get; set; }
    }
}
