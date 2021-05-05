using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Users.Queries
{
    public class UserPermissionStringQuery : IRequest<string>
    {
        public UserPermissionStringQuery(int partyId)
        {
            PartyId = partyId;
        }

        public int PartyId { get; }
    }

    public class UserPermissionStringQueryHandler : IRequestHandler<UserPermissionStringQuery, string>
    {
        private readonly IApplicationDbContext _context;

        public UserPermissionStringQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<string> Handle(UserPermissionStringQuery query, CancellationToken cancellationToken)
        {
            var permissions = await _context.PartyRoleAssociations
                .AsNoTracking()
                .Where(t => t.PartyRoleInvolves.PartyId == query.PartyId && t.IsActive)
                .SelectMany(t => t.PermissionPartyRoleAssociations)
                .Select(t => new { t.Permission.Id, Related = t.Permission.PermissionRelationshipPermissions.Select(r => r.RelatedPermissionId) })
                .ToListAsync();

            var permissionCount = await _context.Permissions.CountAsync();

            var bt = new BitArray(permissionCount + 1);

            var userPermissions = new List<int>();

            foreach (var p in permissions) {
                userPermissions.Add(p.Id);
                userPermissions.AddRange(p.Related);
            }

            Parallel.ForEach(userPermissions.Distinct(), (currentPermission) => {
                bt.Set(currentPermission, true);
            });

            byte[] flagsbyte = new byte[(int)Math.Ceiling((decimal)bt.Count / 8)];
            bt.CopyTo(flagsbyte, 0);

            string permissionString = Convert.ToBase64String(flagsbyte);

            return permissionString;
        }
    }
}
