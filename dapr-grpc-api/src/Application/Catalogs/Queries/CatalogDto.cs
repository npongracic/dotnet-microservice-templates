using SC.API.CleanArchitecture.Application.Common.Mappings;
using SC.API.CleanArchitecture.Domain.Entities;
using System;

namespace SC.API.CleanArchitecture.Application.Catalogs.Queries
{
    public class CatalogDto : IMapFrom<Catalog>
    {
        public Guid Id { get; set; }
        public string SystemName { get; set; }
        public string UserFriendlyName { get; set; }
    }
}
