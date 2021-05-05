using FluentValidation;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Models;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.API.CleanArchitecture.Application.Common;

namespace SC.API.CleanArchitecture.Application.Users.Commands
{
    public class CreatePartyDataForUserCommand : IRequest<(Result Result, string UserId, int PartyId)>
    {
        public long PartyRoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullUsername { get; set; }
    }

    public class CreatePartyDataForUserCommandValidator : AbstractValidator<CreatePartyDataForUserCommand>
    {
        public CreatePartyDataForUserCommandValidator()
        {
            RuleFor(t => t.Username)
                .NotEmpty();

            RuleFor(t => t.FullUsername)
               .NotEmpty();

            RuleFor(t => t.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }

    public class CreatePartyDataForUserCommandHandler : IRequestHandler<CreatePartyDataForUserCommand, (Result Result, string UserId, int PartyId)>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;
        private readonly IIdentityService _identityService;
        private readonly AppSettings _appSettings;

        public CreatePartyDataForUserCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, IIdentityService identityService, AppSettings appSettings)
        {
            _context = context;
            _dateTimeService = dateTimeService;
            _identityService = identityService;
            _appSettings = appSettings;
        }
        public async Task<(Result Result, string UserId, int PartyId)> Handle(CreatePartyDataForUserCommand command, CancellationToken cancellationToken)
        {
            var user = _identityService.FindUserByUsername(command.FullUsername);
            if(user != null)
            {
                return (Result.Success(), user.UserId, user.PartyId);
            }

            command.PartyRoleId = await _context.PartyRoles
                .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Tenant)
                .Select(t => t.Id)
                .FirstAsync();
            
            var dateNow = _dateTimeService.Now;
            var individual = await _context.Parties.Where(x => x.Individual.Username.ToUpper() == command.FullUsername).Include(x => x.Individual).FirstOrDefaultAsync();

            var partyRole = new PartyRole
            {
                IsActive = true,
                PartyRoleTypeId = (int)PartyRoleTypeEnum.Member,
                ValidFrom = dateNow,
                Party = individual ?? new Party
                {
                    GlobalId = Guid.NewGuid(),
                    Individual = new Individual
                    {
                        GivenName = command.FirstName,
                        FamilyName = command.LastName,
                        Username = command.FullUsername
                    }
                }
            };
          

            partyRole.PartyRoleAssociationPartyRoleInvolves.Add(new PartyRoleAssociation() {
                PartyRoleInvolvedWithId = command.PartyRoleId,
                IsActive = true,
                ValidFrom = dateNow,
                PartyRoleAssociationTypeId = (int)PartyRoleAssociationTypeEnum.EntityObjectAccess
            });

            partyRole.PartyRoleContactableVia.Add(
                new PartyRoleContactableVia {
                    ContactMedium = new ContactMedium {
                        ContactMediumClassId = (int)ContactMediumClassEnum.Email,
                        Value = command.Email
                    }
                });

            _context.PartyRoles.Add(partyRole);

            await _context.SaveChangesAsync(cancellationToken);

            var response = await _identityService.CreateUserAsync(individual != null ? individual.Id : partyRole.Party.Individual.Id, command.Username, command.Email);

            return (response.Result, response.UserId, partyRole.Party.Individual.Id);
        }
    }
}
