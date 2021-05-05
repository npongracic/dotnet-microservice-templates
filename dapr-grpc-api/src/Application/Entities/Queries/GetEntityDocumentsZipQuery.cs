using AutoMapper;
using AutoMapper.QueryableExtensions;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Entities.Queries
{
    public class GetEntityDocumentsZipQuery : IRequest<List<FileResult>>
    {
        public GetEntityDocumentsZipQuery(long requestId, long[] documentIds)
        {
            RequestId = requestId;
            DocumentIds = documentIds;
        }
        public long RequestId { get; set; }
        public long[] DocumentIds { get; set; }
    }

    public class GetEntityDocumentZipQueryHandler : IRequestHandler<GetEntityDocumentsZipQuery, List<FileResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEntityDocumentZipQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FileResult>> Handle(GetEntityDocumentsZipQuery query, CancellationToken cancellationToken)
        {
            //var documents = await _context.Entities
            //    .Where(t => t.Id == query.RequestId && !t.IsDeleted)
            //    .SelectMany(t => t.EntityDocuments)
            //    .Select(t => t.Document)
            //    .Where(t => query.DocumentIds.Any(x => x == t.Id))
            //    .ProjectTo<FileResult>(_mapper.ConfigurationProvider)
            //    .ToListAsync();

            //return documents;
            return null;
        }
    }
}
