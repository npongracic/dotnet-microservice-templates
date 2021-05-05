using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<ApplicationRole> ApplicationRoles { get; set; }
        DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
        DbSet<Catalog> Catalogs { get; set; }
        DbSet<CatalogItem> CatalogItems { get; set; }
        DbSet<ContactMedium> ContactMedium { get; set; }
        DbSet<ContactMediumClass> ContactMediumClasses { get; set; }
        DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        DbSet<Entity> Entities { get; set; }
        DbSet<EntityInvolvementRole> EntityInvolvementRoles { get; set; }
        DbSet<EntityLifeCycleHistoryLog> EntityLifeCycleHistoryLogs { get; set; }
        DbSet<EntityOperationLog> EntityOperationLogs { get; set; }
        DbSet<EntityProcessLog> EntityProcessLogs { get; set; }
        DbSet<EntitySpecInvolvRoleTypeUse> EntitySpecInvolvRoleTypeUses { get; set; }
        DbSet<EntitySpecInvolvementRoleType> EntitySpecInvolvementRoleTypes { get; set; }
        DbSet<EntitySpecLifeCycleOperation> EntitySpecLifeCycleOperations { get; set; }
        DbSet<EntitySpecification> EntitySpecifications { get; set; }
        DbSet<IntegrationOrganization> IntegrationOrganizations { get; set; }
        DbSet<IdentificationSchema> IdentificationSchemas { get; set; }
        DbSet<Individual> Individuals { get; set; }
        DbSet<InvolvementRole> InvolvementRoles { get; set; }
        DbSet<InvolvementRoleType> InvolvementRoleTypes { get; set; }
        DbSet<InvolvementRoleTypePartyRoleTypeInvolf> InvolvementRoleTypePartyRoleTypeInvolves { get; set; }
        DbSet<LifeCycle> LifeCycles { get; set; }
        DbSet<LifeCycleClassDefLifeCycleTran> LifeCycleClassDefLifeCycleTrans { get; set; }
        DbSet<LifeCycleClassDefinition> LifeCycleClassDefinitions { get; set; }
        DbSet<LifeCycleStateType> LifeCycleStateTypes { get; set; }
        DbSet<LifeCycleTransition> LifeCycleTransitions { get; set; }
        DbSet<LifeCycleTransitionTable> LifeCycleTransitionTables { get; set; }
        DbSet<MimeType> MimeTypes { get; set; }
        DbSet<Operation> Operations { get; set; }
        DbSet<OperationInvolvementRoleType> OperationInvolvementRoleTypes { get; set; }
        DbSet<Domain.Entities.Organization> Organizations { get; set; }
        DbSet<Party> Parties { get; set; }
        DbSet<PartyRole> PartyRoles { get; set; }
        DbSet<PartyRoleAssociation> PartyRoleAssociations { get; set; }
        DbSet<PartyRoleAssociationType> PartyRoleAssociationTypes { get; set; }
        DbSet<PartyRoleContactableVia> PartyRoleContactableVia { get; set; }
        DbSet<PartyRoleType> PartyRoleTypes { get; set; }
        DbSet<PartyRoleTypeDiscriminator> PartyRoleTypeDiscriminators { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<PermissionGroup> PermissionGroups { get; set; }
        DbSet<PermissionLevel> PermissionLevels { get; set; }
        DbSet<PermissionPartyRoleAssociation> PermissionPartyRoleAssociations { get; set; }
        DbSet<PermissionRelationship> PermissionRelationships { get; set; }
        DbSet<PermissionTemplate> PermissionTemplates { get; set; }
        DbSet<PermissionTemplatePermission> PermissionTemplatePermissions { get; set; }
        DbSet<PermissionType> PermissionTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
