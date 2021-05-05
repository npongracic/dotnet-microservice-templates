using AutoMapper;
using AutoMapper.QueryableExtensions;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{
    public class UserPermissionsQuery : IRequest<List<int>>
    {
        public UserPermissionsQuery(int partyId)
        {
            PartyId = partyId;
        }

        public int PartyId { get; }
    }

    public class UserPermissionsQueryHandler : IRequestHandler<UserPermissionsQuery, List<int>>
    {
        private readonly IApplicationDbContext _context;

        public UserPermissionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<List<int>> Handle(UserPermissionsQuery query, CancellationToken cancellationToken)
        {
            var permissions = await _context.PartyRoleAssociations
                .AsNoTracking()
                .Where(t => t.PartyRoleInvolves.PartyId == query.PartyId && t.IsActive)
                .SelectMany(t => t.PermissionPartyRoleAssociations)
                .Select(t => new { t.Permission.Id, Related = t.Permission.PermissionRelationshipPermissions.Select(r => r.RelatedPermissionId) })
                .ToListAsync();

            var userPermissions = new List<int>();

            foreach (var p in permissions) {
                userPermissions.Add(p.Id);
                userPermissions.AddRange(p.Related);
            }

            return userPermissions.Distinct().ToList();
        }
    }
}
