using SC.API.CleanArchitecture.Application.Common.Models;
using SC.API.CleanArchitecture.Application.Users.Commands;
using SC.API.CleanArchitecture.Domain.Entities;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, string UserId)> CreateUserAsync(int partyId, string userName, string password = null);
        UserPartyDataDto FindUser(string email);
        UserPartyDataDto FindUserByUsername(string username);
        Task<Result> DeleteUserAsync(string userId);
    }
}
