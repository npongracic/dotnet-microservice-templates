using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Security
{
    public class OrganizationOperationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, SecurityObject>
    {
        private readonly OperationsMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrganizationOperationAuthorizationHandler(OperationsMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       SecurityObject resource)
        {
            var activeSecurityContextData = _currentUserService.CurrentSecurityContextData;

            if (activeSecurityContextData.IWPRTypeId == (int)PartyRoleTypeEnum.Organization) {
                if (resource.Involvements.ContainsKey((int)InvolvementRoleTypeEnum.EntityOwner) && resource.Involvements[(int)InvolvementRoleTypeEnum.EntityOwner].Contains(activeSecurityContextData.PRInvWithId)) {
                    var allowedPartyRoleTypes = new List<string>()
                    {
                        AppClaimTypes.Membership,
                        AppClaimTypes.PartyRole
                    };
                    var allowedInvolvementRoles = _mapper.OperationsMap[requirement.Name];

                    foreach (var invRole in allowedInvolvementRoles) {
                        if (resource.Involvements.ContainsKey(invRole)) {
                            var allowedPartyRoles = resource.Involvements[invRole];
                            var userPartyRoles = context.User.FindAll(c => allowedPartyRoleTypes.Contains(c.Type));
                            if (userPartyRoles.Any(p => allowedPartyRoles.Contains(int.Parse(p.Value)))) {
                                context.Succeed(requirement);
                                break;
                            }
                        }
                    }

                }
            }

            return Task.CompletedTask;
        }
    }
}
