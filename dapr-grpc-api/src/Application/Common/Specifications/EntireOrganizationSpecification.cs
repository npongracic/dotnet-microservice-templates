using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SC.API.CleanArchitecture.Application.Common.Specifications
{
    public class EntireOrganizationSpecification : ISpecification<PartyRole>
    {
        public long? Id { get; set; }
        public string Code { get; }
        public string Name { get; }
        public bool OnlyActiveRecords { get; }

        public EntireOrganizationSpecification(
            string code = null,
            string name = null,
            long? id = null,
            bool onlyActiveRecords = true)
        {
            Code = code;
            Name = name?.ToUpper();
            Id = id;
            OnlyActiveRecords = onlyActiveRecords;
        }


        public Expression<Func<PartyRole, bool>> Predicate
        {
            get
            {
                Expression<Func<PartyRole, bool>> predicate = t => true;
                if(OnlyActiveRecords)
                {
                    predicate = predicate.And(p => p.IsActive);
                }
   
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    predicate = predicate.And(p => p.JobCode == Code);
                }

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    predicate = predicate.And(p => p.JobTitle.ToUpper().Contains(Name.ToUpper()));
                }

                if (Id.HasValue)
                {
                    predicate = predicate.And(p => p.Id == Id);
                }

                predicate = predicate.And(p => !p.JobCode.EndsWith("/0"));

                return predicate.Expand();
            }
        }


    }
}
