using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using SC.API.CleanArchitecture.Domain.Enums;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SC.API.CleanArchitecture.Application.Common.Specifications
{
    public class OrganizationSpecification : ISpecification<PartyRole>
    {
        public long? Id { get; set; }
        public string OrganizationIdentificationNumber { get; }
        public string OrganizationTradingName { get; }
        public bool OnlyActiveRecords { get; }
        public int? Level { get; }

        public OrganizationSpecification(
            string organizationIdentificationNumber = null,
            string organizationTradingName = null,
            long? id = null,
            int? level = null,
            bool onlyActiveRecords = true)
        {
            OrganizationIdentificationNumber = organizationIdentificationNumber;
            OrganizationTradingName = organizationTradingName?.ToUpper();
            Id = id;
            OnlyActiveRecords = onlyActiveRecords;
            Level = level;
        }


        public Expression<Func<PartyRole, bool>> Predicate
        {
            get
            {
                Expression<Func<PartyRole, bool>> predicate = t => true;

                if (OrganizationIdentificationNumber != null)
                {
                    predicate = predicate.And(p => p.Party.ExternalId == OrganizationIdentificationNumber
                        && p.Party.ExternalIdIdentificationSchemaId == (int)IdentificationSchemaEnum.City);
                }

                if (OrganizationTradingName != null)
                {
                    predicate = predicate.And(p => !string.IsNullOrWhiteSpace(p.JobTitle) ? p.JobTitle.Contains(OrganizationTradingName) : p.Party.Organization.TradingName.StartsWith(OrganizationTradingName));
                }

                if(Id.HasValue)
                {
                    predicate = predicate.And(p => p.Id == Id);
                }

                if (Level.HasValue)
                {
                    predicate = predicate.And(p => p.Party.Organization.Level == Level);
                }

                predicate = predicate.And(p => p.PartyRoleTypeId == (int)PartyRoleTypeEnum.Organization);
                if(OnlyActiveRecords)
                {
                    predicate = predicate.And(p => p.IsActive);
                }
                
                return predicate.Expand();
            }
        }


    }
}
