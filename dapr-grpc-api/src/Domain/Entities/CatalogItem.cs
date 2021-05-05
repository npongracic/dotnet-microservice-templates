using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public class CatalogItem
    {
        public CatalogItem()
        {
            CatalogItems = new HashSet<CatalogItem>();
        }

        public Guid Id { get; set; }
        public Guid CatalogId { get; set; }
        public Guid? ParentCatalogItemId { get; set; }
    
        public int SortIndex { get; set; }

        public string Value { get; set; }
        public string Metadata { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Catalog Catalog { get; set; }
        public CatalogItem ParentCatalogItem { get; set; }

        public int? RecordedByPartyId { get; set; }
        public virtual Party RecordedByParty { get; set; }
        public ICollection<CatalogItem> CatalogItems { get; set; }
    }
}
