
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using SC.API.CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Infrastructure.Persistence
{
    
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTime;
        private IDbContextTransaction _currentTransaction;

        private readonly AppSettings _appSettings;
        private readonly IServiceProvider _serviceProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService, 
            IDateTimeService dateTime, AppSettings appSettings, IServiceProvider serviceProvider) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _appSettings = appSettings;
            _serviceProvider = serviceProvider;
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<CatalogItem> CatalogItems { get; set; }
        public virtual DbSet<ContactMedium> ContactMedium { get; set; }
        public virtual DbSet<ContactMediumClass> ContactMediumClasses { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityDocument> EntityDocuments { get; set; }
        public virtual DbSet<EntityInvolvementRole> EntityInvolvementRoles { get; set; }
        public virtual DbSet<EntityLifeCycleHistoryLog> EntityLifeCycleHistoryLogs { get; set; }
        public virtual DbSet<EntityOperationLog> EntityOperationLogs { get; set; }
        public virtual DbSet<EntityProcessLog> EntityProcessLogs { get; set; }
        public virtual DbSet<EntitySpecInvolvRoleTypeUse> EntitySpecInvolvRoleTypeUses { get; set; }
        public virtual DbSet<EntitySpecInvolvementRoleType> EntitySpecInvolvementRoleTypes { get; set; }
        public virtual DbSet<EntitySpecLifeCycleOperation> EntitySpecLifeCycleOperations { get; set; }
        public virtual DbSet<EntitySpecification> EntitySpecifications { get; set; }
        public virtual DbSet<IntegrationOrganization> IntegrationOrganizations { get; set; }
        public virtual DbSet<IdentificationSchema> IdentificationSchemas { get; set; }
        public virtual DbSet<Individual> Individuals { get; set; }
        public virtual DbSet<InvolvementRole> InvolvementRoles { get; set; }
        public virtual DbSet<InvolvementRoleType> InvolvementRoleTypes { get; set; }
        public virtual DbSet<InvolvementRoleTypePartyRoleTypeInvolf> InvolvementRoleTypePartyRoleTypeInvolves { get; set; }
        public virtual DbSet<LifeCycle> LifeCycles { get; set; }
        public virtual DbSet<LifeCycleClassDefLifeCycleTran> LifeCycleClassDefLifeCycleTrans { get; set; }
        public virtual DbSet<LifeCycleClassDefinition> LifeCycleClassDefinitions { get; set; }
        public virtual DbSet<LifeCycleStateType> LifeCycleStateTypes { get; set; }
        public virtual DbSet<LifeCycleTransition> LifeCycleTransitions { get; set; }
        public virtual DbSet<LifeCycleTransitionTable> LifeCycleTransitionTables { get; set; }
        public virtual DbSet<MimeType> MimeTypes { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<OperationInvolvementRoleType> OperationInvolvementRoleTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<PartyRole> PartyRoles { get; set; }
        public virtual DbSet<PartyRoleAssociation> PartyRoleAssociations { get; set; }
        public virtual DbSet<PartyRoleAssociationType> PartyRoleAssociationTypes { get; set; }
        public virtual DbSet<PartyRoleContactableVia> PartyRoleContactableVia { get; set; }
        public virtual DbSet<PartyRoleType> PartyRoleTypes { get; set; }
        public virtual DbSet<PartyRoleTypeDiscriminator> PartyRoleTypeDiscriminators { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }
        public virtual DbSet<PermissionLevel> PermissionLevels { get; set; }
        public virtual DbSet<PermissionPartyRoleAssociation> PermissionPartyRoleAssociations { get; set; }
        public virtual DbSet<PermissionRelationship> PermissionRelationships { get; set; }
        public virtual DbSet<PermissionTemplate> PermissionTemplates { get; set; }
        public virtual DbSet<PermissionTemplatePermission> PermissionTemplatePermissions { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            //await OnAfterSaveChanges(auditEntries);
            return result;
        }
        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch {
                RollbackTransaction();
                throw;
            }
            finally {
                if (_currentTransaction != null) {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public void RollbackTransaction()
        {
            try {
                _currentTransaction?.Rollback();
            }
            finally {
                if (_currentTransaction != null) {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed mutually dependent data

            // Grad
            builder.Entity<Party>().HasData(new Party[] {
                new Party {
                    Id = 1,
                    GlobalId = new Guid("3cebb7da-4cfa-46e7-be74-66df1e4a6e7c"),
                    ExternalId = "61817894937",
                    ExternalIdIdentificationSchemaId = (int)IdentificationSchemaEnum.City
                }
            });

            // Service Account
            builder.Entity<Party>().HasData(new Party[] {
                new Party {
                    Id = 2,
                    GlobalId = new Guid("a3149834-b9c7-497a-bb1c-206504a17aa4"),
                    ExternalId = "-",
                    ExternalIdIdentificationSchemaId = (int)IdentificationSchemaEnum.HrVat,
                }
            });

            builder.Entity<Individual>().HasData(new Individual[] {
                new Individual
                {
                    Id = 2,
                    GivenName= "SCSA",
                    FamilyName= "2",
                    Username = "ServiceAccount"
                }
            }); 


            builder.Entity<PartyRole>().HasData(new PartyRole[]
            {
                new PartyRole
                {
                    IsActive = true,
                    Id = 1,
                    PartyId = 1,
                    ValidFrom = new DateTime(2020, 12, 21, 14, 46, 19, 263, DateTimeKind.Utc).AddTicks(7881),
                    PartyRoleTypeId = (int)PartyRoleTypeEnum.Tenant,
                }
            });

            base.OnModelCreating(builder);
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry, _dateTime)
                {
                    TableName = entry.Metadata.GetTableName(), 
                    RecordedByParty = _currentUserService?.Username
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    // The following condition is ok with EF Core 2.2 onwards.
                    // If you are using EF Core 2.1, you may need to change the following condition to support navigation properties: https://github.com/dotnet/efcore/issues/17700
                    // if (property.IsTemporary || (entry.State == EntityState.Added && property.Metadata.IsForeignKey()))
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }
                }
            }

            return auditEntries.ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            var temporaryAuditEntries = auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
            foreach (var auditEntry in temporaryAuditEntries)
            {
                // Get the final value of the temporary properties
                auditEntry.ToAudit();
   
                // Save the Audit entry or publish to a queue/stream
            }

            return Task.CompletedTask;
        }
    }

    public class AuditEntry
    {
        private readonly IDateTimeService _dateTime;
        public AuditEntry(EntityEntry entry, IDateTimeService dateTime)
        {
            Entry = entry;
            _dateTime = dateTime;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public string RecordedByParty { get; set; }

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            var audit = new Audit
            {
                TableName = TableName,
                DateTime = _dateTime.Now,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                RecordedByParty = RecordedByParty
            };

            return audit;
        }
    }
}
