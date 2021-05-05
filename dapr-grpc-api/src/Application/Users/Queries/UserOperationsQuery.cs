using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{
    public class UserOperationsQuery : IRequest<Dictionary<string, List<int>>>
    {
    }

    public class UserOperationsQueryHandler : IRequestHandler<UserOperationsQuery, Dictionary<string, List<int>>>
    {
        private readonly IApplicationDbContext _context;

        public UserOperationsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<Dictionary<string, List<int>>> Handle(UserOperationsQuery query, CancellationToken cancellationToken)
        {
            var res = await _context.Operations
                .AsNoTracking()
                .GroupBy(t => t.Name)
                .ToDictionaryAsync(kv => kv.Key, kv => kv.SelectMany(t => t.OperationInvolvementRoleTypes.Select(i => i.OperationId)).ToList());

            return res;
        }
    }
}
