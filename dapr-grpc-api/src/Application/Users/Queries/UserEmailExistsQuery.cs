using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{
    public class UserEmailExistsQuery : IRequest<bool>
    {
        public string Email { get; private set; }

        public UserEmailExistsQuery(string email)
        {
            Email = email;
        }
    }

    public class UserEmailExistsQueryHandler : IRequestHandler<UserEmailExistsQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public UserEmailExistsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<bool> Handle(UserEmailExistsQuery query, CancellationToken cancellationToken)
        {
            var email = query.Email.Trim().ToUpper();

            var exist = await _context.ApplicationUsers
                .Where(t => t.NormalizedEmail == email)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return false;
        }
    }
}
