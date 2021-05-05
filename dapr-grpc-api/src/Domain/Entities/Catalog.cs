using SC.API.CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class Catalog 
    {
        public Catalog()
        {
            CatalogItems = new HashSet<CatalogItem>();
            Catalogs = new HashSet<Catalog>();
        }

        public Guid Id { get; set; }
        public Guid? ParentCatalogId { get; set; }
        public string SystemName { get; set; }
        public string UserFriendlyName { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsUserDefinedSorting { get; set; }
        public bool IsAlphabeticalSorting { get; set; }

        public ICollection<CatalogItem> CatalogItems { get; set; }
        public ICollection<Catalog> Catalogs { get; set; }
        public virtual Catalog ParentCatalog { get; set; }
    }
}
