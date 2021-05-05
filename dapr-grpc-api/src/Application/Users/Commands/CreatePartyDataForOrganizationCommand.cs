using FluentValidation;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Commands
{
    public class CreatePartyDataForOrganizationCommand : IRequest<int>
    {
        public string Oib { get; set; }
        public string LongName { get; set; }
        public string AddressStreetAndBuildingNumber { get; set; }
        public string AddressPostalOfficeCode { get; set; }
        public string AddressCity { get; set; }
    }

    public class CreatepartyDataForOrganizationCommandValidator : AbstractValidator<CreatePartyDataForOrganizationCommand>
    {
        public CreatepartyDataForOrganizationCommandValidator()
        {
            RuleFor(t => t.Oib)
                .Length(11);
        }
    }

    public class CreatePartyDataForOrganizationCommandHandler : IRequestHandler<CreatePartyDataForOrganizationCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public CreatePartyDataForOrganizationCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }
        public async Task<int> Handle(CreatePartyDataForOrganizationCommand command, CancellationToken cancellationToken)
        {
            var dateNow = _dateTimeService.Now;

            var partyRole = new PartyRole
            {
                IsActive = true,
                PartyRoleTypeId = (int)PartyRoleTypeEnum.Organization,
                ValidFrom = dateNow,
                Party = new Party
                {
                    GlobalId = Guid.NewGuid(),
                    ExternalId = command.Oib,
                    ExternalIdIdentificationSchemaId = (int) IdentificationSchemaEnum.HrVat,
                    Organization = new Domain.Entities.Organization
                    {
                        LongName = command.LongName,
                        AddressStreetAndBuildingNumber = command.AddressStreetAndBuildingNumber,
                        AddressPostalOfficeCode = command.AddressPostalOfficeCode,
                        AddressCity = command.AddressCity,
                        ExistingDuringFrom = dateNow
                    }
                }
            };

            _context.PartyRoles.Add(partyRole);
            await _context.SaveChangesAsync(cancellationToken);

            return partyRole.PartyId.Value;
        }
    }
}
