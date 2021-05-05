using SC.API.CleanArchitecture.Application.Common.Exceptions;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.CatalogItems.Commands
{
    public class DeleteCatalogItemCommand : IRequest
    {
        public DeleteCatalogItemCommand(Guid catalogItemId) 
        {
            CatalogItemId = catalogItemId;
        }

        public Guid CatalogItemId { get; set; }
    }

    public class DeleteCatalogItemCommandHandler : IRequestHandler<DeleteCatalogItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public DeleteCatalogItemCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }
        public async Task<Unit> Handle(DeleteCatalogItemCommand command, CancellationToken cancellationToken)
        {
            var dateNow = _dateTimeService.Now;

            var catalogItem = await _context.CatalogItems.FirstOrDefaultAsync(t => t.Id == command.CatalogItemId);

            if (catalogItem == null) {
                throw new NotFoundException($"Catalog item with Id: {command.CatalogItemId} does not exist in database!");
            }

            catalogItem.IsDeleted = true;
            catalogItem.DeletedDate = dateNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
