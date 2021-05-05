using AutoMapper;
using AutoMapper.QueryableExtensions;
using SC.API.CleanArchitecture.Application.Common;
using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Application.Common.Specifications;
using SC.API.CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.CatalogItems.Queries
{
    public class GetCatalogItemsQuery : IRequest<List<CatalogItemDto>>
    {
        public GetCatalogItemsQuery(CatalogItemSpecification specification, QueryOptions queryOptions)
        {
            Specification = specification;
            QueryOptions = queryOptions;
        }

        public CatalogItemSpecification Specification { get; set; }
        public QueryOptions QueryOptions { get; set; }
    }

    public class GetCatalogItemsQueryHandler : IRequestHandler<GetCatalogItemsQuery, List<CatalogItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const int MAX_DEPTH = 4;

        public GetCatalogItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private static Expression<Func<CatalogItem, CatalogItemDto>> GetCatalogItemProjection(int maxDepth, bool onlyActiveRecords, int currentDepth = 0)
        {
            currentDepth++;
            Expression<Func<CatalogItem, CatalogItemDto>> result = catalogItem => new CatalogItemDto()
            {
                Id = catalogItem.Id,
                Value = catalogItem.Id,
                Label = catalogItem.Value,
                SortIndex = catalogItem.SortIndex,
                IsDeleted = catalogItem.IsDeleted,
                Children = currentDepth == maxDepth 
                ? new List<CatalogItemDto>()
                : onlyActiveRecords ? catalogItem.CatalogItems.AsQueryable().Where(x => !x.IsDeleted).Select(GetCatalogItemProjection(maxDepth, onlyActiveRecords, currentDepth))
                    .OrderBy(y => y.SortIndex).ToList() : 
                    catalogItem.CatalogItems.AsQueryable().Select(GetCatalogItemProjection(maxDepth, onlyActiveRecords, currentDepth))
                    .OrderBy(y => y.SortIndex).ToList()
            };

            return result;
        }

        public async  Task<List<CatalogItemDto>> Handle(GetCatalogItemsQuery query, CancellationToken cancellationToken)
        {
            if(query.Specification.CatalogItemId.HasValue)
            {
                var catalogItems = await _context.CatalogItems
                    .Where(query.Specification.Predicate)
                    .ProjectTo<CatalogItemDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                
                return catalogItems;
            }
            else
            {
                var catalogItems = await _context.CatalogItems
                    .Where(query.Specification.Predicate)
                    .Where(x => x.ParentCatalogItem == null).Select(GetCatalogItemProjection(MAX_DEPTH, query.Specification.OnlyActiveRecords, 0))
                    //.OrderBy(x => x.SortIndex)
                    .WithQueryOptions(query.QueryOptions, x => x.SortIndex)
                    .ToListAsync(cancellationToken);

                return catalogItems;
            }
        }
    }
}
