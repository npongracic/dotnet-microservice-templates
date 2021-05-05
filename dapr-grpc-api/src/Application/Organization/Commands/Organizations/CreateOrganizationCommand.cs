using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Organization.Commands.Organizations
{
    public class CreateOrganizationCommand : IRequest<long>
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationExternalId { get; set; }
    }

    public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationCommandValidator()
        {
            RuleFor(t => t.OrganizationCode).NotEmpty();
            RuleFor(t => t.OrganizationName).NotEmpty();
            RuleFor(t => t.OrganizationExternalId).NotEmpty();
        }
    }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public CreateOrganizationCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }
        public async Task<long> Handle(CreateOrganizationCommand command, CancellationToken cancellationToken)
        {
            var now = _dateTimeService.Now;
            var organizationExists = await _context.PartyRoles.Where(t => t.Party.ExternalId == command.OrganizationExternalId && 
                t.Party.ExternalIdIdentificationSchemaId == (int)IdentificationSchemaEnum.City).FirstOrDefaultAsync();
            
            if (organizationExists != null)
            {
                return organizationExists.Id;
            }

            var tenantPartyRoleId = await _context.PartyRoles
                .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Tenant)
                .Select(t => t.Id)
                .FirstAsync();

            var organization = new PartyRole
            {
                IsActive = true,
                PartyRoleTypeId = (int)PartyRoleTypeEnum.Organization,
                ValidFrom = now,
                JobCode = command.OrganizationCode,
                JobTitle = command.OrganizationName,
                Party = new Party
                {
                    GlobalId = Guid.NewGuid(),
                    ExternalId = command.OrganizationExternalId,
                    ExternalIdIdentificationSchemaId = (int)IdentificationSchemaEnum.City,
                    Organization = new Domain.Entities.Organization
                    {
                        Code = command.OrganizationCode,
                        TradingName = command.OrganizationName,
                        LongName = command.OrganizationName,
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

            _context.PartyRoles.Add(organization);
            await _context.SaveChangesAsync(cancellationToken);

            return organization.Id;
        }
    }
}
