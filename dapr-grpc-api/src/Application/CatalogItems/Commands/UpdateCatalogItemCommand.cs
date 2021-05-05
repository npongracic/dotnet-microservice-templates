using FluentValidation;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using SC.API.CleanArchitecture.Application.Common.Exceptions;

namespace SC.API.CleanArchitecture.Application.CatalogItems.Commands
{
    public class UpdateCatalogItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid? ParentCatalogItemId { get; set; }
        public string Value { get; set; }
        public int SortIndex { get; set; }
    }

    public class UpdateCatalogItemCommandValidator : AbstractValidator<UpdateCatalogItemCommand>
    {
        public UpdateCatalogItemCommandValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
            RuleFor(t => t.Value).NotEmpty();
        }
    }

    public class UpdateCatalogItemCommandHandler : IRequestHandler<UpdateCatalogItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCatalogItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateCatalogItemCommand command, CancellationToken cancellationToken)
        {
            var catalogItem = await _context.CatalogItems
                .FirstOrDefaultAsync(t => t.Id == command.Id);

            if (catalogItem == null) {
                throw new NotFoundException($"Cannot find CatalogItem with Id: {command.Id}");
            }

            catalogItem.Value = command.Value;
            catalogItem.SortIndex = command.SortIndex;
            catalogItem.ParentCatalogItemId = command.ParentCatalogItemId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
