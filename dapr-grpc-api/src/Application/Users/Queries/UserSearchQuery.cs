using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Specifications;
using SC.API.CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{
    public class UserQueryDto
    {
        public int PartyRoleAssociationId { get; set; }
        public int PartyId { get; set; }
        public long PartyRoleId { get; set; }
        public string UserName { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int CompanyId { get; set; }
        public long CompanyPartyRoleId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyIdentificationNumber { get; set; }
        public string CompanyAddressStreetAndBuildingNumber { get; set; }
        public string CompanyAddressPostalOfficeCode { get; set; }
        public string CompanyAddressCity { get; set; }
        public string CompanyAddressLine { get; set; }
        public bool Deleted { get; set; }
    }

    public class UserSearchQuery : IRequest<List<UserQueryDto>>
    {
        public UserSearchQuery(OrganizationUserSpecification specification, QueryOptions queryOptions)
        {
            Specification = specification;
            QueryOptions = queryOptions;
        }

        public OrganizationUserSpecification Specification { get; }
        public QueryOptions QueryOptions { get; }
    }

    public class UsersSearchQueryHandler : IRequestHandler<UserSearchQuery, List<UserQueryDto>>
    {
        private readonly IApplicationDbContext _context;

        public UsersSearchQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserQueryDto>> Handle(UserSearchQuery query, CancellationToken cancellationToken)
        {
            var users = await _context.PartyRoleAssociations
                .Where(query.Specification.Predicate)
                .Select(t => new UserQueryDto() {
                    CompanyAddressStreetAndBuildingNumber = t.PartyRoleInvolvedWith.Party.Organization.AddressStreetAndBuildingNumber,
                    CompanyAddressCity = t.PartyRoleInvolvedWith.Party.Organization.AddressCity,
                    CompanyAddressLine = t.PartyRoleInvolvedWith.Party.Organization.AddressLine,
                    CompanyId = t.PartyRoleInvolvedWith.Party.Organization.Id,
                    CompanyIdentificationNumber = t.PartyRoleInvolvedWith.Party.ExternalId,
                    CompanyName = t.PartyRoleInvolvedWith.Party.Organization.TradingName,
                    CompanyPartyRoleId = t.PartyRoleInvolvedWith.Id,
                    PartyId = t.PartyRoleInvolves.PartyId.Value,
                    PartyRoleId = t.PartyRoleInvolves.Id,
                    PartyRoleAssociationId = t.Id,
                    PhoneNumber = t.PartyRoleInvolves.PartyRoleContactableVia.Select(c => c.ContactMedium).FirstOrDefault(c => c.ContactMediumClassId == (int)ContactMediumClassEnum.Mobile).Value,
                    UserName = t.PartyRoleInvolves.Party.Users.FirstOrDefault().UserName,
                    GivenName = t.PartyRoleInvolves.Party.Individual.GivenName,
                    MiddleName = t.PartyRoleInvolves.Party.Individual.MiddleName,
                    FamilyName = t.PartyRoleInvolves.Party.Individual.FamilyName,
                })
                .WithQueryOptions(query.QueryOptions, t => t.UserName)
                .ToListAsync(cancellationToken);

            return users;
                    
        }
    }
}
