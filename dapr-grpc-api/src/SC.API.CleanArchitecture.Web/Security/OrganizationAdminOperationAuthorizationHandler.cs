using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Security
{
    public class OrganizationAdminOperationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, SecurityObject>
    {
        private readonly OperationsMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrganizationAdminOperationAuthorizationHandler(OperationsMapper mapper, ICurrentUserService currentUserService)
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
                    if (context.User.HasClaim(c => c.Type == AppClaimTypes.EntityObjectAdministration && resource.Involvements.ContainsKey((int)InvolvementRoleTypeEnum.EntityOwner) && resource.Involvements[(int)InvolvementRoleTypeEnum.EntityOwner].Contains(int.Parse(c.Value)))) {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;

        }
    }

}
