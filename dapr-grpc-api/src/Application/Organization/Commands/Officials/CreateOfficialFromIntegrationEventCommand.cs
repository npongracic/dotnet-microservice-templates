using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Exceptions;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Organization.Commands.Organizations;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Organization.Commands.Officials
{
    public class CreateOfficialFromIntegrationEventCommand : IRequest<long>
    {
        public string OfficialExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string OrganizationExternalId { get; set; }
        public string OrganizationCode { get; set; }
        public string JobTitle { get; set; }
    }

    public class CreateOfficialFromIntegrationEventCommandValidator : AbstractValidator<CreateOfficialFromIntegrationEventCommand>
    {
        public CreateOfficialFromIntegrationEventCommandValidator()
        {
            RuleFor(t => t.FirstName).NotEmpty();
            RuleFor(t => t.Username).NotEmpty();
            RuleFor(t => t.OrganizationCode).NotEmpty();
            RuleFor(t => t.JobTitle).NotEmpty();
        }
    }

    public class CreateOfficialFromIntegrationEventCommandHandler : IRequestHandler<CreateOfficialFromIntegrationEventCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public CreateOfficialFromIntegrationEventCommandHandler(
            IApplicationDbContext context,
            AppSettings appSettings,
            ICurrentUserService currentUserService,
            IDateTimeService dateTimeService
        )
        {
            _context = context;
            _appSettings = appSettings;
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public async Task<long> Handle(CreateOfficialFromIntegrationEventCommand command, CancellationToken cancellationToken)
        {
            var now = _dateTimeService.Now;

            var tenantPartyRoleId = await _context.PartyRoles
                .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Tenant)
                .Select(t => t.Id)
                // Extra conditions to filter the specific tenant you want, not first async
                .FirstAsync();

            var organizationPartyRole = await _context.PartyRoles
                            .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Organization && (t.Party.ExternalId == command.OrganizationExternalId || t.Party.Organization.Code == command.OrganizationCode) &&
                            t.IsActive == true)
                            .FirstOrDefaultAsync();

            if (organizationPartyRole == null)
            {
                var organizationName = command.JobTitle.Split(',');
                if(organizationName.Length > 2 && !string.IsNullOrWhiteSpace(organizationName[2]))
                {
                    organizationPartyRole = await Helpers.CreateOrganization(_context, now, command.OrganizationCode, organizationName[2], command.OrganizationExternalId);
                }
            }

            var partyRole = new PartyRole
            {
                IsActive = true,
                PartyRoleTypeId = (int)PartyRoleTypeEnum.Member,
                ValidFrom = now,
                JobCode = command.JobTitle.Substring(0, command.JobTitle.IndexOf(',', StringComparison.Ordinal)),
                JobTitle = command.JobTitle,
                Party = new Party
                {
                    GlobalId = Guid.NewGuid(),
                    ExternalId = command.OfficialExternalId,
                    ExternalIdIdentificationSchemaId = 3,
                    Individual = new Individual
                    {
                        GivenName = command.FirstName,
                        FamilyName = command.LastName,
                        Username = command.Username,
                        AliveDuringFrom = now
                    }
                }
            };

            partyRole.PartyRoleAssociationPartyRoleInvolves.Add(new PartyRoleAssociation()
            {
                PartyRoleInvolvedWithId = tenantPartyRoleId,
                IsActive = true,
                ValidFrom = now,
                PartyRoleAssociationTypeId = (int)PartyRoleAssociationTypeEnum.EntityObjectAccess
            });

            partyRole.PartyRoleAssociationPartyRoleInvolves.Add(new PartyRoleAssociation()
            {
                PartyRoleInvolvedWith = organizationPartyRole,
                IsActive = true,
                ValidFrom = now,
                PartyRoleAssociationTypeId = (int)PartyRoleAssociationTypeEnum.Membership
            });

            _context.PartyRoles.Add(partyRole);

            await _context.SaveChangesAsync(cancellationToken);
            return partyRole.Id;
        }
    }
}
