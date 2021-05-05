using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Models;
using SC.API.CleanArchitecture.Application.Users.Commands;
using SC.API.CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public IdentityService(UserManager<ApplicationUser> userManager)
        {   
            _userManager = userManager;
        }
        
        public async Task<(Result Result, string UserId)> CreateUserAsync(int partyId, string username, string email)
        {
            var user = new ApplicationUser {
                UserName = username,
                Email = email,
                PartyId = partyId
            };

            var result = await _userManager.CreateAsync(user);
            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null) {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public UserPartyDataDto FindUser(string email)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            if(user != null)
            {
                return new UserPartyDataDto
                {
                    PartyId = user.PartyId,
                    UserId = user.Id
                };
            }

            return null;
        }

        public UserPartyDataDto FindUserByUsername(string username)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.NormalizedUserName == username.ToUpper());
            if (user != null)
            {
                return new UserPartyDataDto
                {
                    PartyId = user.PartyId,
                    UserId = user.Id
                };
            }

            return null;
        }
    }
}
