using AutoMapper;
using SC.API.CleanArchitecture.Application.Common.Mappings;
using SC.API.CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Application.CatalogItems.Queries
{
    public class CatalogItemDto : IMapFrom<CatalogItem>
    {
        public Guid Id { get; set; }
        public Guid? ParentCatalogItemId { get; set; }
        public string ParentCatalogItemName { get; set; }
        public string Label { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Value { get; set; }
        public int SortIndex { get; set; }
        public ICollection<CatalogItemDto> Children { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CatalogItem, CatalogItemDto>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Value))
                .ForMember(d => d.ParentCatalogItemName, o => o.MapFrom(s => s.ParentCatalogItem.Value))
                .ForMember(d => d.Children, o => o.MapFrom(s => s.CatalogItems));
        }
    }
}
