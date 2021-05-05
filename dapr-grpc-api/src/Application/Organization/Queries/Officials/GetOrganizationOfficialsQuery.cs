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

namespace SC.API.CleanArchitecture.Application.Organization.Queries.Officials
{
    public class GetOrganizationOfficialsQuery : IRequest<PageableCollection<OfficialDto>>
    {
        public GetOrganizationOfficialsQuery(OfficialSpecification specification, QueryOptions queryOptions)
        {
            Specification = specification;
            QueryOptions = queryOptions;
        }

        public OfficialSpecification Specification { get; set; }
        public QueryOptions QueryOptions { get; set; }
    }

    public class GetOrganizationOfficialsQueryHandler : IRequestHandler<GetOrganizationOfficialsQuery, PageableCollection<OfficialDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetOrganizationOfficialsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PageableCollection<OfficialDto>> Handle(GetOrganizationOfficialsQuery query, CancellationToken cancellationToken)
        {
            var officials = await _context.PartyRoles
                .Where(query.Specification.Predicate)
                //.WithRequestDomainVisibility(_currentUserService)
                .ProjectTo<OfficialDto>(_mapper.ConfigurationProvider)
                .WithQueryOptions(query.QueryOptions, t => t.Code, true)
                .ToListAsync();

            var total = await _context.PartyRoles
                .Where(query.Specification.Predicate)
                //.WithRequestDomainVisibility(_currentUserService)
                .LongCountAsync();

            return new PageableCollection<OfficialDto>(officials, total);
        }
    }
}
