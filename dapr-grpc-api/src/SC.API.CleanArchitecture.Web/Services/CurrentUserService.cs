using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Users.Commands;
using SC.API.CleanArchitecture.Application.Users.Queries;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        IPrincipal _principal;
  
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IMediator mediator, AppSettings appSettings)
        {
            _principal = httpContextAccessor.HttpContext?.User;

            if (_principal != null) {
                var claimsPrincipal = ((ClaimsPrincipal)_principal);

                UserId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
                Surname = claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value;
                GivenName = claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value;
                UserFullName = claimsPrincipal.FindFirst(x => x.Type == "name")?.Value;
                var userName = claimsPrincipal.FindFirst(x => x.Type == "preferred_username")?.Value;
                if(!string.IsNullOrWhiteSpace(userName))
                {
                    var domainPrefix = $@"{appSettings.DomainPrefix ?? "INTRANET"}\";
                    Username = userName.ToLower().StartsWith(domainPrefix.ToLower()) ? userName : $"{domainPrefix}{userName}";
                }
            }
        }

      
        public string Username { get; }
        public string GivenName { get; }
        public string Surname { get; }

        public string UserId { get; }

        public string UserFullName { get; }
        public string Email { get; }

        public int PartyId
        {
            get
            {
                var partyId = ((ClaimsPrincipal)_principal)?.FindFirst(AppClaimTypes.Party)?.Value;
                return !string.IsNullOrWhiteSpace(partyId) ? int.Parse(partyId) : default;
            }
        }

        public IEnumerable<SecurityContextData> SecurityContextData
        {
            get
            {
                var claim = ((ClaimsPrincipal)_principal).Claims.FirstOrDefault(t => t.Type == AppClaimTypes.SecurityContextData);

                if (claim == null) {
                    return new List<SecurityContextData>();
                }

                var scds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SecurityContextData>>(claim.Value);

                return scds;
            }
        }

        public SecurityContextData CurrentSecurityContextData => SecurityContextData.FirstOrDefault(t => t.IsActiveContext);

        public IEnumerable<Claim> Claims => ((ClaimsPrincipal)_principal).Claims;


        public IEnumerable<long> GetPartyRoleIds(PartyRoleTypeEnum partyRoleType)
        {
            var claims = ((ClaimsPrincipal)_principal).FindAll(partyRoleType.ToString());

            var partyIds = claims.Select(t => long.Parse(t.Value)).ToList();

            return partyIds;
        }
    }
}
