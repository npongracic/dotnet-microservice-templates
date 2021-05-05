using MediatR;
using Microsoft.AspNetCore.Authentication;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Users.Commands;
using SC.API.CleanArchitecture.Application.Users.Queries;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Infrastructure.Identity
{

    public class AddPartyInfoClaimsTransformation : IClaimsTransformation
    {
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public AddPartyInfoClaimsTransformation(IMediator mediator, AppSettings appSettings)
        {
            _mediator = mediator;
            _appSettings = appSettings;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Clone current identity
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            var lastName = principal.FindFirst(ClaimTypes.Surname)?.Value;
            var firstName = principal.FindFirst(ClaimTypes.GivenName)?.Value;
            var partyId = principal.FindFirst(AppClaimTypes.Party)?.Value;
            var username = principal.FindFirst(x => x.Type == "preferred_username")?.Value;
            var domainPrefix = $@"{_appSettings.DomainPrefix ?? "INTRANET"}\";
            var fullUsername = username.ToUpper().StartsWith(domainPrefix.ToUpper()) ? username.ToUpper() : $"{domainPrefix}{username}".ToUpper();

            if (!string.IsNullOrWhiteSpace(partyId))
            {
                return principal;
            }

            try
            {
                await _mediator.Send(new CreatePartyDataForUserCommand()
                {
                    Email = email,
                    Username = username,
                    FirstName = firstName,
                    LastName = lastName,
                    FullUsername = fullUsername
                });
            }
            catch (Exception ex)
            { 
            }

            var claims = await _mediator.Send(new UserClaimsQuery(userId, fullUsername, email));
            newIdentity.AddClaims(claims);

            return clone;
        }
    }
}
