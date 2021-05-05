using FluentValidation;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Commands
{
    public class DeactivatePartyRoleAssociationsCommand : IRequest
    {
        public long PartyRoleAssociationId { get; set; }
    }

    public class DeactivatePartyRoleAssociationsCommandHandler : IRequestHandler<DeactivatePartyRoleAssociationsCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeactivatePartyRoleAssociationsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeactivatePartyRoleAssociationsCommand command, CancellationToken cancellationToken)
        {
            var partyRoleAssociation = await _context.PartyRoleAssociations
                .Where(t => t.Id == command.PartyRoleAssociationId && t.IsActive == true)
                .FirstAsync();

            partyRoleAssociation.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
