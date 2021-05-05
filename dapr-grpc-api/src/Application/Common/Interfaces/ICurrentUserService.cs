using SC.API.CleanArchitecture.Application.Users.Queries;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using System.Collections.Generic;
using System.Security.Claims;

namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Username { get; }
        string Email { get; }
        string UserFullName { get; }
        public string GivenName { get; }
        public string Surname { get; }
        int PartyId { get; }
        IEnumerable<SecurityContextData> SecurityContextData { get; }
        SecurityContextData CurrentSecurityContextData { get; }
        IEnumerable<Claim> Claims { get; }

        IEnumerable<long> GetPartyRoleIds(PartyRoleTypeEnum partyRoleType);
    }
}
