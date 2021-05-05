using AutoMapper;
using AutoMapper.QueryableExtensions;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{
    public class UserClaimsQuery : IRequest<List<Claim>>
    {
        public UserClaimsQuery(string userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }

        public string UserId { get; }
        public string Email { get; }
        public string Username { get; }
    }

    public class UserClaimsQueryHandler : IRequestHandler<UserClaimsQuery, List<Claim>>
    {
        private readonly IApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserClaimsQueryHandler(IApplicationDbContext context, AppSettings appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        public async  Task<List<Claim>> Handle(UserClaimsQuery query, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>();

            var individual = await _context.ApplicationUsers
                .AsNoTracking()
                .Where(t => t.NormalizedUserName == query.Username.ToUpper())
                .Select(t => t.Party.Individual)
                .FirstAsync();

            //claims.Add(new Claim(ClaimTypes.GivenName, individual.GivenName, ClaimValueTypes.String));
            //claims.Add(new Claim(ClaimTypes.Surname, individual.FamilyName, ClaimValueTypes.String));

            //claims.Add(new Claim(AppClaimTypes.FullName, individual.FullName, ClaimValueTypes.String));

            claims.Add(new Claim(AppClaimTypes.Party, individual.Id.ToString(), ClaimValueTypes.Integer32));

            var userPartyRoles = await _context.PartyRoles
                .Include(t => t.Party)
                .Include(t => t.PartyRoleType)
                .Include(t => t.PartyRoleContactableVia)
                    .ThenInclude(t => t.ContactMedium)
                .Include(t => t.PartyRoleAssociationPartyRoleInvolves)
                    .ThenInclude(t => t.PartyRoleAssociationType)
                .Include(t => t.PartyRoleAssociationPartyRoleInvolves)
                    .ThenInclude(t => t.PartyRoleInvolvedWith)
                    .ThenInclude(t => t.Party)
                    .ThenInclude(t => t.Organization)
                .Where(t => t.PartyId == individual.Id)
                .Where(t => t.IsActive)
                .Where(t => !string.IsNullOrWhiteSpace(t.JobCode))
                .AsNoTracking()
                .ToListAsync();

            var organization = userPartyRoles
                .SelectMany(t => t.PartyRoleAssociationPartyRoleInvolves)
                .Where(t => t.PartyRoleAssociationTypeId == (int)PartyRoleAssociationTypeEnum.EntityObjectAccess)
                .Select(pra => pra.PartyRoleInvolvedWith)
                .Where(p => p.Party.ExternalIdIdentificationSchemaId == (int)IdentificationSchemaEnum.HrVat)
                .FirstOrDefault();

            if (organization != null) {
                var contactableVia = userPartyRoles
                    .Where(t => t.PartyRoleTypeId == (int)PartyRoleTypeEnum.Member)
                    .SelectMany(t => t.PartyRoleContactableVia)
                    .Where(t => t.ContactMedium.ContactMediumClassId == (int)ContactMediumClassEnum.Mobile)
                    .Select(t => t.ContactMedium.Value)
                    .FirstOrDefault();

                claims.Add(new Claim(AppClaimTypes.OrganizationIdentificationNumber, organization.Party.ExternalId, ClaimValueTypes.String));
                //claims.Add(new Claim(AppClaimTypes.OrganizationTradingName, organization.Party.Organization.TradingName, ClaimValueTypes.String));
                //claims.Add(new Claim(AppClaimTypes.OrganizationAddressLine, organization.Party.Organization.AddressLine, ClaimValueTypes.String));
                //claims.Add(new Claim(AppClaimTypes.UserContactPhone, contactableVia ?? string.Empty, ClaimValueTypes.String));

                //claims.Add(new Claim(AppClaimTypes.IsDomainUser, (organization.PartyRoleTypeId == (int)PartyRoleTypeEnum.GZGOrganization).ToString(), ClaimValueTypes.Boolean));
            }            

            foreach (var partyRole in userPartyRoles) {
                claims.Add(new Claim(AppClaimTypes.PartyRole, partyRole.Id.ToString(), ClaimValueTypes.Integer64));

                claims.Add(new Claim(((PartyRoleTypeEnum)partyRole.PartyRoleTypeId).ToString(), partyRole.Id.ToString(), ClaimValueTypes.Integer64));

                foreach (var partyRoleInvolve in partyRole.PartyRoleAssociationPartyRoleInvolves.Where(t => t.IsActive && t.PartyRoleAssociationTypeId != (int)PartyRoleAssociationTypeEnum.EntityObjectAccess && t.PartyRoleAssociationTypeId != (int)PartyRoleAssociationTypeEnum.ServiceProviding)) {
                    var partyRoleAssociationType = (PartyRoleAssociationTypeEnum)partyRoleInvolve.PartyRoleAssociationType.Id;

                    claims.Add(new Claim(partyRoleAssociationType.ToString(), partyRoleInvolve.PartyRoleInvolvedWithId.ToString(), ClaimValueTypes.Integer64));
                }
            }

            var partyRoleTypeIds = new List<int>()
            {
                (int)PartyRoleTypeEnum.Organization,
                (int)PartyRoleTypeEnum.ServiceProvider,
                (int)PartyRoleTypeEnum.PlatformOwner,
                (int)PartyRoleTypeEnum.Tenant
            };

            var partyRoleAssociations = await _context.PartyRoleAssociations
                .Include(t => t.PartyRoleInvolvedWith.Party)
                .Include(t => t.PartyRoleInvolvedWith.Party.Organization)
                .Include(t => t.PartyRoleInvolvedWith.PartyRoleType)
                .Where(t => t.IsActive && t.PartyRoleInvolves.PartyId == individual.Id
                    && t.PartyRoleAssociationTypeId == (int)PartyRoleAssociationTypeEnum.EntityObjectAccess
                    && t.PartyRoleInvolves.PartyRoleTypeId == (int)PartyRoleTypeEnum.Member
                    && partyRoleTypeIds.Contains(t.PartyRoleInvolvedWith.PartyRoleTypeId))
                .ToListAsync();

            var scds = new List<SecurityContextData>();

            foreach (var pra in partyRoleAssociations)
            {
                var org = pra.PartyRoleInvolvedWith.Party.Organization;

                if (org != null)
                {
                    scds.Add(new SecurityContextData()
                    {
                        PRAssId = pra.Id,
                        PRInvWithId = pra.PartyRoleInvolvedWithId,
                        IWPRTypeId = pra.PartyRoleInvolvedWith.PartyRoleTypeId,
                        IWPartyName = org.TradingName,
                        IWPRTypeName = pra.PartyRoleInvolvedWith.PartyRoleType.Name,
                        UserPRId = pra.PartyRoleInvolvesId
                    });
                }
            }

            scds.First(t => t.IWPRTypeId == (int)PartyRoleTypeEnum.Organization || t.IWPRTypeId == (int)PartyRoleTypeEnum.Tenant).IsActiveContext = true;

            claims.Add(new Claim(AppClaimTypes.SecurityContextData, JsonSerializer.Serialize(scds)));

            return claims;
        }
    }
}
