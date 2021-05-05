using AutoMapper;
using AutoMapper.QueryableExtensions;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Organization.Queries.Organizations
{
    public class GetOrganizationsQuery : IRequest<PageableCollection<OrganizationDto>>
    {
        public GetOrganizationsQuery(OrganizationSpecification specification, QueryOptions queryOptions)
        {
            Specification = specification;
            QueryOptions = queryOptions;
        }

        public OrganizationSpecification Specification { get; set; }
        public QueryOptions QueryOptions { get; set; }
    }

    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, PageableCollection<OrganizationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetOrganizationsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PageableCollection<OrganizationDto>> Handle(GetOrganizationsQuery query, CancellationToken cancellationToken)
        {
            var officials = await _context.PartyRoles
                .Where(query.Specification.Predicate)
                //.WithRequestDomainVisibility(_currentUserService)
                .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider)
                .WithQueryOptions(query.QueryOptions, t => t.Name, true)
                .ToListAsync();

            var total = await _context.PartyRoles
                .Where(query.Specification.Predicate)
                //.WithRequestDomainVisibility(_currentUserService)
                .LongCountAsync();

            return new PageableCollection<OrganizationDto>(officials, total);
        }
    }
}
