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
    public class UpdateOfficialFromIntegrationEventCommand : IRequest<Unit>
    {
        public string OfficialExternalId { get; set; }
        public string Username { get; set; }
        public string OrganizationExternalId { get; set; }
        public string OrganizationCode { get; set; }
        public string JobTitle { get; set; }
    }

    public class UpdateOfficialFromIntegrationEventCommandValidator : AbstractValidator<UpdateOfficialFromIntegrationEventCommand>
    {
        public UpdateOfficialFromIntegrationEventCommandValidator()
        {
            RuleFor(t => t.Username).NotEmpty();
            RuleFor(t => t.OrganizationCode).NotEmpty();
            RuleFor(t => t.JobTitle).NotEmpty();
        }
    }

    public class UpdateOfficialFromIntegrationEventCommandHandler : IRequestHandler<UpdateOfficialFromIntegrationEventCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public UpdateOfficialFromIntegrationEventCommandHandler(
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

        public async Task<Unit> Handle(UpdateOfficialFromIntegrationEventCommand command, CancellationToken cancellationToken)
        {
            var now = _dateTimeService.Now;
            var jobCode = command.JobTitle.Substring(0, command.JobTitle.IndexOf(',', StringComparison.Ordinal));

            var organizationPartyRole = await _context.PartyRoles
                .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Organization && (t.Party.ExternalId == command.OrganizationExternalId || t.Party.Organization.Code == command.OrganizationCode) &&
                t.IsActive == true)
                .FirstOrDefaultAsync();

            if(organizationPartyRole == null)
            {
                var organizationName = command.JobTitle.Split(',');
                if (organizationName.Length > 2 && !string.IsNullOrWhiteSpace(organizationName[2]))
                {
                    organizationPartyRole = await Helpers.CreateOrganization(_context, now, command.OrganizationCode, organizationName[2], command.OrganizationExternalId);
                }
            }

            var officialPartyRole = await _context.PartyRoles
                .Include(x => x.PartyRoleAssociationPartyRoleInvolves)
                .Include(x => x.Party)
                    .ThenInclude(x => x.Individual)
                .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Member && t.Party.ExternalId == command.OfficialExternalId && t.IsActive == true)
                .FirstAsync();

            if(officialPartyRole.JobCode != jobCode)
            {
                foreach (var association in officialPartyRole.PartyRoleAssociationPartyRoleInvolves.Where(p => p.PartyRoleAssociationTypeId == (int)PartyRoleAssociationTypeEnum.Membership))
                {
                    association.IsActive = false;
                    association.ValidDue = now;
                }

                _context.PartyRoleAssociations.Add(new PartyRoleAssociation()
                {
                    PartyRoleInvolvedWith = organizationPartyRole,
                    PartyRoleInvolvesId = officialPartyRole.Id,
                    IsActive = true,
                    ValidFrom = now,
                    PartyRoleAssociationTypeId = (int)PartyRoleAssociationTypeEnum.Membership
                });

                officialPartyRole.JobCode = jobCode;
            }

            if(officialPartyRole.JobTitle != command.JobTitle)
            {
                officialPartyRole.JobTitle = command.JobTitle;
            }

            if(officialPartyRole.Party.Individual != null && officialPartyRole.Party.Individual.Username != command.Username)
            {
                officialPartyRole.Party.Individual.Username = command.Username;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
