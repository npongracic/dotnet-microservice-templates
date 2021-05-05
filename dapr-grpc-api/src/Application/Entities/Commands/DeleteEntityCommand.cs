using SC.API.CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Entities.Commands
{
    public class DeleteEntityCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteEntityCommandHandler : IRequestHandler<DeleteEntityCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public DeleteEntityCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }
        public async Task<Unit> Handle(DeleteEntityCommand command, CancellationToken cancellationToken)
        {
            var dateNow = _dateTimeService.Now;

            var entity = await _context.Entities
                .FirstOrDefaultAsync(t => t.Id == command.Id);

            if (entity == null)
                throw new Exception($"Cannot execute delete because Entity with Id: {command.Id} doesn't exist");

            entity.IsDeleted = true;
            entity.DeletedDate = dateNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
