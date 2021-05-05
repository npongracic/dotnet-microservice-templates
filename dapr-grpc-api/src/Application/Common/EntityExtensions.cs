using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using System;
using System.Linq;

namespace SC.API.CleanArchitecture.Application.Common
{
    public static class EntityExtensions
    {
        public static IQueryable<TSource> WithEntitySecurity<TSource>(this IQueryable<TSource> queryable, ICurrentUserService currentUserService, OperationEnum? operation = null, AppSettings settings = null) where TSource : Entity
        {
            var contextData = currentUserService.CurrentSecurityContextData;
            var claims = currentUserService.Claims;

            if (contextData.IWPRTypeId == (int)PartyRoleTypeEnum.PlatformOwner)
            {
                return queryable;
            }

            
            if (contextData.IWPRTypeId == (int)PartyRoleTypeEnum.Tenant)
            {
                // Treba provjerit je li involvement rola AssignedTo, Associate ili AssociateReadOnly jedna partyrole korisnika
                // Ako to ne prođe, treba provjeriti je li partyrole od korisnika member partyrole od involved with na kojem je entitet assignedto ili je pak partyrol korisnika member assignedto na entitetu

                //if (currentUserService.Permissions.CanSeeTeamAssignments)
                //{
                //    var partyRoleClaims = claims
                //    .Where(t => t.Type == AppClaimTypes.PartyRole || t.Type == AppClaimTypes.Membership || t.Type == AppClaimTypes.EntityObjectAdministration)
                //    .Select(t => t.Value)
                //    .ToList();

                //    queryable = queryable.Where(t => t.EntityInvolvementRoles.Any(e => e.InvolvementRole.IsActive
                //       && (e.InvolvementRole.InvolvementRoleTypeId == (int)InvolvementRoleTypeEnum.AssignedTo ||
                //       e.InvolvementRole.InvolvementRoleTypeId == (int)InvolvementRoleTypeEnum.Associate ||
                //       e.InvolvementRole.InvolvementRoleTypeId == (int)InvolvementRoleTypeEnum.AssociateReadOnly)

                //       && partyRoleClaims.Contains(e.InvolvementRole.PartyRoleId.ToString())));

                //    return queryable;
                //}
   
                //if (currentUserService.Permissions.CanSeeOnlyAssignedTo)
                //{
                //    var partyRoleClaims = claims
                //    .Where(t => t.Type == AppClaimTypes.PartyRole)
                //    .Select(t => t.Value)
                //    .ToList();

                //    queryable = queryable.Where(t => t.EntityInvolvementRoles.Any(e => e.InvolvementRole.IsActive
                //        && (e.InvolvementRole.InvolvementRoleTypeId == (int)InvolvementRoleTypeEnum.AssignedTo ||
                //        e.InvolvementRole.InvolvementRoleTypeId == (int)InvolvementRoleTypeEnum.Associate ||
                //        e.InvolvementRole.InvolvementRoleTypeId == (int)InvolvementRoleTypeEnum.AssociateReadOnly)

                //        && partyRoleClaims.Contains(e.InvolvementRole.PartyRoleId.ToString())));

                //    return queryable;
                //}
                
                return queryable;
            }

            return queryable;
        }

        public static void AddEntityOwner(this Entity entity, ICurrentUserService currentUserService, DateTime now)
        {
            var contextData = currentUserService.CurrentSecurityContextData;

            var involvementRole = new InvolvementRole
            {
                PartyRoleId = contextData.PRInvWithId,
                ValidFrom = now,
                IsActive = true,
                InvolvementRoleTypeId = (int)InvolvementRoleTypeEnum.EntityOwner
            };

            entity.EntityInvolvementRoles.Add(new EntityInvolvementRole
            {
                InvolvementRole = involvementRole
            });
        }

        public static void AddEntityOwner(this Entity entity, long partyRoleId, DateTime now)
        {
            var involvementRole = new InvolvementRole
            {
                PartyRoleId = partyRoleId,
                ValidFrom = now,
                IsActive = true,
                InvolvementRoleTypeId = (int)InvolvementRoleTypeEnum.EntityOwner
            };

            entity.EntityInvolvementRoles.Add(new EntityInvolvementRole
            {
                InvolvementRole = involvementRole
            });

        }

        public static void AddEntityInvolvementRole(this Entity entity, long partyRoleId, InvolvementRoleTypeEnum role, DateTime now)
        {
            var involvementRole = new InvolvementRole
            {
                PartyRoleId = partyRoleId,
                ValidFrom = now,
                IsActive = true,
                InvolvementRoleTypeId = (int)role
            };
            

            entity.EntityInvolvementRoles.Add(new EntityInvolvementRole
            {
                InvolvementRole = involvementRole
            });
        }

        public static void AddProcessLogItem(this Entity entity, ICurrentUserService currentUserService, DateTime now, string note)
        {
            entity.EntityProcessLogs.Add(new EntityProcessLog
            {
                ExecutorName = currentUserService.UserFullName,
                Note = note,
                Timestamp = now
            });
        }
    }
}
