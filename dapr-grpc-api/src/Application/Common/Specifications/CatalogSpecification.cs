using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using LinqKit;
using System;
using System.Linq.Expressions;

namespace SC.API.CleanArchitecture.Application.Common.Specifications
{
    public class CatalogSpecification : ISpecification<Catalog>
    {
        public CatalogSpecification(
            Guid? catalogId,
            DateTimeOffset? dateFrom,
            DateTimeOffset? dateTo,
            string systemName,
            string userFriendlyName,
            bool onlyActiveRecords = true)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            CatalogId = catalogId;
            SystemName = systemName?.Trim().ToLower();
            UserFriendlyName = userFriendlyName?.Trim().ToLower();
            OnlyActiveRecords = onlyActiveRecords;
        }

        public Guid? CatalogId { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
        public string SystemName { get; set; }
        public string UserFriendlyName { get; set; }
        public bool OnlyActiveRecords { get; set; }

        public Expression<Func<Catalog, bool>> Predicate
        {
            get
            {
                Expression<Func<Catalog, bool>> predicate = t => true;
                if (OnlyActiveRecords)
                {
                    predicate = predicate.And(t => !t.IsDeleted);
                }

                if (CatalogId.HasValue)
                {
                    predicate = predicate.And(t => t.Id == CatalogId.Value);
                }

                //if (DateFrom.HasValue)
                //{
                //    predicate = predicate.And(t => t.CreatedDate.Value.Date >= DateFrom.Value.Date);
                //}

                //if (DateTo.HasValue)
                //{
                //    predicate = predicate.And(t => t.CreatedDate.Value.Date <= DateTo.Value.Date);
                //}

                if (!string.IsNullOrWhiteSpace(SystemName))
                {
                    predicate = predicate.And(t => t.SystemName.Trim().ToLower() == SystemName);
                }

                if (!string.IsNullOrWhiteSpace(UserFriendlyName))
                {
                    predicate = predicate.And(t => t.UserFriendlyName.Trim().ToLower() == UserFriendlyName);
                }

                return predicate.Expand();
            }
        }
    }
}
