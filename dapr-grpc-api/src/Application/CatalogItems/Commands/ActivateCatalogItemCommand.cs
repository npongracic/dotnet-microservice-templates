using SC.API.CleanArchitecture.Application.Common.Exceptions;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.CatalogItems.Commands
{
    public class ActivateCatalogItemCommand : IRequest
    {
        public Guid CatalogItemId { get; set; }
    }
    public class ActivateCatalogItemCommandHandler : IRequestHandler<ActivateCatalogItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public ActivateCatalogItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(ActivateCatalogItemCommand command, CancellationToken cancellationToken)
        {
            var catalogItem = await _context.CatalogItems.FirstOrDefaultAsync(t => t.Id == command.CatalogItemId);

            if (catalogItem == null) {
                throw new NotFoundException($"CatalogItem with Id: {command.CatalogItemId} does not exist in database!");
            }

            catalogItem.IsDeleted = false;
            catalogItem.DeletedDate = null;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
