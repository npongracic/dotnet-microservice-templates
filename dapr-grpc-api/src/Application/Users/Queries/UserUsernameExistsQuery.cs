using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{
    public class UserUsernameExistsQuery : IRequest<bool>
    {
        public string Username { get; private set; }

        public UserUsernameExistsQuery(string username)
        {
            Username = username;
        }
    }

    public class UserUsernameExistsQueryHandler : IRequestHandler<UserUsernameExistsQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public UserUsernameExistsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<bool> Handle(UserUsernameExistsQuery query, CancellationToken cancellationToken)
        {
            var username = query.Username.Trim().ToUpper();

            var exists = await _context.ApplicationUsers
                .Where(t => t.NormalizedUserName == username)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return false;
        }
    }
}
