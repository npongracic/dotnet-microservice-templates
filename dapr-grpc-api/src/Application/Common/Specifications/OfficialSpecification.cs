using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SC.API.CleanArchitecture.Application.Common.Specifications
{
    public class OfficialSpecification : ISpecification<PartyRole>
    {
        public long? Id { get; set; }
        public string Code { get; }
        public string Name { get; }
        public bool IsActive { get; }

        public OfficialSpecification(
            string code = null,
            string name = null,
            long? id = null,
            bool isActive = true)
        {
            Code = code;
            Name = name;
            Id = id;
            IsActive = isActive;
        }


        public Expression<Func<PartyRole, bool>> Predicate
        {
            get
            {
                Expression<Func<PartyRole, bool>> predicate = t => true;

                if (!string.IsNullOrWhiteSpace(Code))
                {
                    predicate = predicate.And(p => p.JobCode == Code);
                }

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    predicate = predicate.And(p => p.JobTitle.ToUpper().Contains(Name.ToUpper()));
                }

                if(Id.HasValue)
                {
                    predicate = predicate.And(p => p.Id == Id);
                }

                predicate = predicate.And(p => p.PartyRoleTypeId == (int)PartyRoleTypeEnum.Member);
                predicate = predicate.And(p => p.IsActive == IsActive);

                return predicate.Expand();
            }
        }


    }
}
