using AutoMapper;
using AutoMapper.QueryableExtensions;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Catalogs.Queries
{
    public class GetCatalogsQuery : IRequest<PageableCollection<CatalogDto>>
    {
        public GetCatalogsQuery(CatalogSpecification specification, QueryOptions queryOptions)
        {
            Specification = specification;
            QueryOptions = queryOptions;
        }

        public CatalogSpecification Specification { get; set; }
        public QueryOptions QueryOptions { get; set; }
    }

    public class GetCatalogsQueryHandler : IRequestHandler<GetCatalogsQuery, PageableCollection<CatalogDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCatalogsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageableCollection<CatalogDto>> Handle(GetCatalogsQuery query, CancellationToken cancellationToken)
        {
            var catalogs = await _context.Catalogs.Where(query.Specification.Predicate)
                .ProjectTo<CatalogDto>(_mapper.ConfigurationProvider)
                .WithQueryOptions(query.QueryOptions, t => t.UserFriendlyName)
                .ToListAsync(cancellationToken);

            var total = await _context.Catalogs.Where(query.Specification.Predicate)
                .ProjectTo<CatalogDto>(_mapper.ConfigurationProvider)
                .LongCountAsync(cancellationToken);


            return new PageableCollection<CatalogDto>(catalogs, total);
        }
    }
}
