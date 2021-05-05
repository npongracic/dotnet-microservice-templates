using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Entities.Commands
{
    public class ActivateEntityCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class ActivateEntityCommandHandler : IRequestHandler<ActivateEntityCommand>
    {
        private readonly IApplicationDbContext _context;

        public ActivateEntityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(ActivateEntityCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Entities
                .FirstOrDefaultAsync(t => t.Id == command.Id);

            if (entity == null)
                throw new Exception($"Cannot activate Entity because Entity with Id: {command.Id} doesn't exist");

            entity.IsDeleted = false;
            entity.DeletedDate = null;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
