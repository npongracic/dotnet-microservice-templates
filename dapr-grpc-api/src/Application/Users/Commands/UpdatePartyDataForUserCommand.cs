using FluentValidation;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Commands
{
    public class UpdatePartyDataForUserCommand : IRequest
    {
        public int PartyRoleId { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdatePartyDataForUserCommandValidator : AbstractValidator<UpdatePartyDataForUserCommand>
    {
        public UpdatePartyDataForUserCommandValidator()
        {
            RuleFor(t => t.FamilyName)
                .NotEmpty();

            RuleFor(t => t.GivenName)
                .NotEmpty();

            RuleFor(t => t.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(t => t.PhoneNumber)
                .NotEmpty();
        }
    }

    public class UpdatePartyDataForUserCommandHandler : IRequestHandler<UpdatePartyDataForUserCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePartyDataForUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdatePartyDataForUserCommand command, CancellationToken cancellationToken)
        {
            var individual = await _context.PartyRoles
                .Where(t => t.Id == command.PartyRoleId)
                .Select(t => t.Party.Individual)
                .FirstAsync();

            individual.GivenName = command.GivenName;
            individual.MiddleName = command.MiddleName;
            individual.FamilyName = command.FamilyName;

            var phoneNumber = await _context.ContactMedium
                .Where(t => t.PartyRoleContactableVia.Any(p => p.PartyRoleId == command.PartyRoleId) && t.ContactMediumClassId == (int)ContactMediumClassEnum.Mobile)
                .FirstAsync();

            phoneNumber.Value = command.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
