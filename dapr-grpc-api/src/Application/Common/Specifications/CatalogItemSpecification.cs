using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using LinqKit;
using System;
using System.Linq.Expressions;

namespace SC.API.CleanArchitecture.Application.Common.Specifications
{
    public class CatalogItemSpecification : ISpecification<CatalogItem>
    {
        public CatalogItemSpecification(
            Guid? catalogId,
            Guid? catalogItemId,
            DateTimeOffset? dateFrom,
            DateTimeOffset? dateTo,
            string systemName,
            string userFriendlyName,
            string value,
            bool onlyActiveRecords = true)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            CatalogId = catalogId;
            SystemName = systemName?.Trim().ToLower();
            UserFriendlyName = userFriendlyName?.Trim().ToLower();
            CatalogItemId = catalogItemId;
            Value = !string.IsNullOrEmpty(value) ? value.Trim().ToLower() : null;
            OnlyActiveRecords = onlyActiveRecords;
        }

        public Guid? CatalogId { get; set; }
        public Guid? CatalogItemId { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
        public string SystemName { get; set; }
        public string UserFriendlyName { get; set; }
        public string Value { get; set; }
        public bool OnlyActiveRecords { get; set; }

        public Expression<Func<CatalogItem, bool>> Predicate
        {
            get
            {
                Expression<Func<CatalogItem, bool>> predicate = t => true;
                if(OnlyActiveRecords)
                {
                    predicate = predicate.And(t => !t.IsDeleted);
                }
 
                if (CatalogId.HasValue)
                {
                    predicate = predicate.And(t => t.Catalog.Id == CatalogId.Value);
                }


                if (CatalogItemId.HasValue)
                {
                    predicate = predicate.And(t => t.Id == CatalogItemId.Value);
                }

                //if (DateFrom.HasValue)
                //{
                //    predicate = predicate.And(t => t..Value.Date >= DateFrom.Value.Date);
                //}

                //if (DateTo.HasValue)
                //{
                //    predicate = predicate.And(t => t.CreatedDate.Value.Date <= DateTo.Value.Date);
                //}

                if (!string.IsNullOrWhiteSpace(SystemName))
                {
                    predicate = predicate.And(t => t.Catalog.SystemName.Trim().ToLower() == SystemName);
                }

                if (!string.IsNullOrWhiteSpace(Value))
                {
                    predicate = predicate.And(t => t.Value.Trim().Contains(Value));
                }

                if (!string.IsNullOrWhiteSpace(UserFriendlyName))
                {
                    predicate = predicate.And(t => t.Catalog.UserFriendlyName.Trim().ToLower() == UserFriendlyName);
                }

                return predicate.Expand();
            }
        }
    }
}
