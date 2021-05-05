using FluentValidation;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SC.API.CleanArchitecture.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace SC.API.CleanArchitecture.Application.CatalogItems.Commands
{
    public class CreateCatalogItemCommand : IRequest<Guid>
    {
        public Guid CatalogId { get; set; }
        public Guid? ParentCatalogItemId { get; set; }
        public string Value { get; set; }
        public int SortIndex { get; set; }
    }

    public class CreateCatalogItemCommandValidator : AbstractValidator<CreateCatalogItemCommand>
    {
        public CreateCatalogItemCommandValidator()
        {
            RuleFor(t => t.CatalogId).NotEmpty();
            RuleFor(t => t.Value).NotEmpty();
        }
    }

    public class CreateCatalogItemCommandHandler : IRequestHandler<CreateCatalogItemCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;
        private readonly ICurrentUserService _currentUserService;

        public CreateCatalogItemCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUserService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateCatalogItemCommand command, CancellationToken cancellationToken)
        {
            var dateNow = _dateTimeService.Now;

            var catalogItemExists = await _context.CatalogItems.Where(t => t.CatalogId == command.CatalogId && t.Value.ToLower().Trim() == command.Value.ToLower().Trim()).FirstOrDefaultAsync();

            if (catalogItemExists != null) {
                if(catalogItemExists.IsDeleted)
                {
                    catalogItemExists.IsDeleted = false;
                    catalogItemExists.RecordedByPartyId = _currentUserService.PartyId;
                    await _context.SaveChangesAsync(cancellationToken);

                    return catalogItemExists.Id;
                }
                else
                {
                    throw new DuplicateItemException($"CatalogItem with name {command.Value} already exists!");
                }
            }

            var catalogItem = new CatalogItem
            {
                CatalogId = command.CatalogId,
                ParentCatalogItemId = command.ParentCatalogItemId,
                Value = command.Value,
                Id = Guid.NewGuid(),
                SortIndex = command.SortIndex,
                RecordedByPartyId = _currentUserService.PartyId
            };

            _context.CatalogItems.Add(catalogItem);

            await _context.SaveChangesAsync(cancellationToken);

            return catalogItem.Id;
        }
    }
}
