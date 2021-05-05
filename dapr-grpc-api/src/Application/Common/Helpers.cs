using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Common
{
    public static class Helpers
    {
        public static string GetFullName(string givenName, string middleName, string familyName)
        {
            return givenName + " " + (middleName != null ? middleName + " ": "") + familyName;
        }

        public static string GetAddressLine(string streetAndBuildingNumber, string postalCode, string city)
        {
            return streetAndBuildingNumber + ", " + (postalCode != null ? postalCode + ", " : "") + (city ?? "");
        }

        public static Entity CreateEntity(IApplicationDbContext context)
        {
            const int specId = (int)EntitySpecificationEnum.Ticket;

            var lifeCycleId = context.LifeCycles
               .Where(t => t.LifeCycleStateTypeId == (int)LifeCycleStateTypeEnum.Initial && t.LifeCycleClassDefinition.EntitySpecifications.Any(e => e.Id == specId))
               .Select(t => t.Id)
               .FirstOrDefault();

            var entity = new Entity
            {
                RecordId = Guid.NewGuid(),
                EntitySpecificationId = 1,
                LifeCycleChangeTime = DateTime.UtcNow,
                RecordedByPartyId = 2,
                LifeCycleId = lifeCycleId,
                RecordedTimeStamp = DateTime.UtcNow
            };

            //var documentDefinition = new DocumentDefinition
            //{
            //    Id = (int)DocumentDefinitionEnum.GeneralDocument,
            //    Name = "GeneralDocument"
            //};

            //var mimeType = new DocumentExtension
            //{
            //    Extension = ".pdf",
            //    FullTextSearchable = false
            //};

            //context.DocumentDefinitions.Add(documentDefinition);

            //context.DocumentExtensions.Add(mimeType);

            //context.Entities.Add(entity);

            return entity;
        }

        public static async Task<int> GetOrCreateTicketStatus(IApplicationDbContext context, int statusCode, string statusName, LifeCycleStateTypeEnum stateType = LifeCycleStateTypeEnum.Transition)
        {
            var ticketStatus = await context.LifeCycles.Where(t => t.Id == statusCode).FirstOrDefaultAsync();
            if (ticketStatus == null)
            {
                var status = new LifeCycle
                {
                    Id = statusCode,
                    Name = statusName ?? statusCode.ToString(),
                    LifeCycleClassDefinitionId = (int)LifeCycleClassDefinitionEnum.TicketLifeCycle,
                    LifeCycleStateTypeId = (int)stateType
                };

                context.LifeCycles.Add(status);
            }

            return statusCode;
        }

        public static async Task<PartyRole> CreateOrganization(IApplicationDbContext context, DateTime now, string organizationCode, string organizationName, string organizationExternalId)
        {
            var tenantPartyRoleId = await context.PartyRoles
                .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Tenant)
                .Select(t => t.Id)
                .FirstAsync();

            var organization = new PartyRole
            {
                IsActive = true,
                PartyRoleTypeId = (int)PartyRoleTypeEnum.Organization,
                ValidFrom = now,
                JobCode = organizationCode,
                JobTitle = organizationName,
                Party = new Party
                {
                    GlobalId = Guid.NewGuid(),
                    ExternalId = organizationExternalId,
                    ExternalIdIdentificationSchemaId = (int)IdentificationSchemaEnum.HrVat,
                    Organization = new Domain.Entities.Organization
                    {
                        Code = organizationCode,
                        TradingName = organizationName,
                        LongName = organizationName,
                        ExistingDuringFrom = now
                    }
                }
            };

            organization.PartyRoleAssociationPartyRoleInvolves.Add(new PartyRoleAssociation()
            {
                PartyRoleInvolvedWithId = tenantPartyRoleId,
                IsActive = true,
                ValidFrom = now,
                PartyRoleAssociationTypeId = (int)PartyRoleAssociationTypeEnum.EntityObjectAccess
            });

            context.PartyRoles.Add(organization);

            return organization;
        }
    }
}
