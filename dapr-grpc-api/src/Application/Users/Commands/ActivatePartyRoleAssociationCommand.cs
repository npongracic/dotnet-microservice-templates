using FluentValidation;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Commands
{
    public class ActivatePartyRoleAssociationsCommand : IRequest
    {
        public long PartyRoleAssociationId { get; set; }
    }

    public class ActivatePartyRoleAssociationsCommandHandler : IRequestHandler<ActivatePartyRoleAssociationsCommand>
    {
        private readonly IApplicationDbContext _context;

        public ActivatePartyRoleAssociationsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(ActivatePartyRoleAssociationsCommand command, CancellationToken cancellationToken)
        {
            var partyRoleAssociation = await _context.PartyRoleAssociations
                .Where(t => t.Id == command.PartyRoleAssociationId && t.IsActive == false)
                .FirstAsync();

            partyRoleAssociation.IsActive = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
