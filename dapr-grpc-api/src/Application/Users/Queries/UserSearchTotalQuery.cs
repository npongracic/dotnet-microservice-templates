using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{

    public class UserSearchTotalQuery : IRequest<int>
    {
        public UserSearchTotalQuery(OrganizationUserSpecification specification)
        {
            Specification = specification;
        }

        public OrganizationUserSpecification Specification { get; }
    }

    public class UsersSearchTotalQueryHandler : IRequestHandler<UserSearchTotalQuery, int>
    {
        private readonly IApplicationDbContext _context;

        public UsersSearchTotalQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UserSearchTotalQuery query, CancellationToken cancellationToken)
        {
            var total = await _context.PartyRoleAssociations
                .Where(query.Specification.Predicate)
                .CountAsync(cancellationToken);

            return total;
        }
    }
}
