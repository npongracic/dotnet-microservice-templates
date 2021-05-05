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
    public class UpdateOfficialAccessFromIntegrationEventCommand : IRequest<Unit>
    {
        public long OfficialPartyRoleId { get; set; }
        public long OrganizationPartyRoleId { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateOfficialAccessFromIntegrationEventCommandValidator : AbstractValidator<UpdateOfficialAccessFromIntegrationEventCommand>
    {
        public UpdateOfficialAccessFromIntegrationEventCommandValidator()
        {
            RuleFor(t => t.OfficialPartyRoleId).NotEmpty();
            RuleFor(t => t.OrganizationPartyRoleId).NotEmpty();
        }
    }

    public class UpdateOfficialAccessFromIntegrationEventCommandHandler : IRequestHandler<UpdateOfficialAccessFromIntegrationEventCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public UpdateOfficialAccessFromIntegrationEventCommandHandler(
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

        public async Task<Unit> Handle(UpdateOfficialAccessFromIntegrationEventCommand command, CancellationToken cancellationToken)
        {
            var now = _dateTimeService.Now;

            var association = await _context.PartyRoleAssociations.Where(x => x.PartyRoleInvolvesId == command.OfficialPartyRoleId && x.PartyRoleInvolvedWithId == command.OrganizationPartyRoleId).FirstOrDefaultAsync();
            association.IsActive = command.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
