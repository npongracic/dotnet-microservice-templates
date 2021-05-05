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
    public class CreateOfficialAccessFromIntegrationEventCommand : IRequest<Unit>
    {
        public long OfficialPartyRoleId { get; set; }
        public long OrganizationPartyRoleId { get; set; }
        public DateTime ValidFrom { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateOfficialAccessFromIntegrationEventCommandValidator : AbstractValidator<CreateOfficialAccessFromIntegrationEventCommand>
    {
        public CreateOfficialAccessFromIntegrationEventCommandValidator()
        {
            RuleFor(t => t.OfficialPartyRoleId).NotEmpty();
            RuleFor(t => t.OrganizationPartyRoleId).NotEmpty();
            RuleFor(t => t.ValidFrom).NotEmpty();
        }
    }

    public class CreateOfficialAccessFromIntegrationEventCommandHandler : IRequestHandler<CreateOfficialAccessFromIntegrationEventCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public CreateOfficialAccessFromIntegrationEventCommandHandler(
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

        public async Task<Unit> Handle(CreateOfficialAccessFromIntegrationEventCommand command, CancellationToken cancellationToken)
        {
            var now = _dateTimeService.Now;

            _context.PartyRoleAssociations.Add(new PartyRoleAssociation()
            {
                PartyRoleInvolvedWithId = command.OrganizationPartyRoleId,
                PartyRoleInvolvesId = command.OfficialPartyRoleId,
                IsActive = command.IsActive,
                ValidFrom = command.ValidFrom,
                PartyRoleAssociationTypeId = (int)PartyRoleAssociationTypeEnum.EntityObjectAdministration
            });

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
