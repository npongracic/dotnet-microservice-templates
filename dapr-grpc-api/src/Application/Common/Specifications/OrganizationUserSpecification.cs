using SC.API.CleanArchitecture.Application.Common.Interfaces;
using SC.API.CleanArchitecture.Domain.Entities;
using System;
using System.Linq.Expressions;
using LinqKit;
using SC.API.CleanArchitecture.Domain.Enums;
using System.Linq;

namespace SC.API.CleanArchitecture.Application.Common.Specifications
{
    public class OrganizationUserSpecification : ISpecification<PartyRoleAssociation>
    {
        public OrganizationUserSpecification (
            string organizationIdentificationNumber = null,
            string organizationTradingName = null,
            string officialJobCode = null,
            string officialExternalID = null,
            long? partyRoleID = null,
            string fullName = null,
            bool isActive = true)
        {
            OrganizationIdentificationNumber = organizationIdentificationNumber;
            OrganizationTradingName = organizationTradingName?.ToLower();
            OfficialJobCode = officialJobCode;
            OfficialExternalID = officialExternalID;
            FullName = fullName?.ToUpper();
            IsActive = isActive;
            PartyRoleID = partyRoleID;
        }


        public Expression<Func<PartyRoleAssociation, bool>> Predicate
        {
            get
            {
                Expression<Func<PartyRoleAssociation, bool>> predicate = t => true;

                predicate = predicate.And(p => p.PartyRoleAssociationTypeId == (int)PartyRoleAssociationTypeEnum.Membership
                    && p.PartyRoleInvolves.IsActive 
                    && p.PartyRoleInvolves.PartyRoleTypeId == (int)PartyRoleTypeEnum.Member
                    && p.PartyRoleInvolvedWith.IsActive
                    && p.PartyRoleInvolvedWith.Party.ExternalIdIdentificationSchemaId == (int)IdentificationSchemaEnum.City);


                if (OrganizationIdentificationNumber != null) {
                    predicate = predicate.And(p => p.PartyRoleInvolvedWith.Party.ExternalId == OrganizationIdentificationNumber 
                        && p.PartyRoleInvolvedWith.Party.ExternalIdIdentificationSchemaId == (int)IdentificationSchemaEnum.City);
                }

                if (OrganizationTradingName != null) {
                    predicate = predicate.And(p => p.PartyRoleInvolvedWith.Party.Organization.TradingName.StartsWith(OrganizationTradingName));
                }

                if (OfficialJobCode != null) {
                    predicate = predicate.And(p => p.PartyRoleInvolves.JobCode == OfficialJobCode);
                }

                if (OfficialJobCode != null)
                {
                    predicate = predicate.And(p => p.PartyRoleInvolves.Party.ExternalId == OfficialExternalID);
                }

                if (PartyRoleID.HasValue)
                {
                    predicate = predicate.And(p => p.PartyRoleInvolves.Id == PartyRoleID);
                }
                //predicate = predicate.And(p => p.PartyRoleInvolvedWith.PartyRoleTypeId == (int)PartyRoleTypeEnum.Tenant);
                predicate = predicate.And(p => p.IsActive == IsActive);

                return predicate.Expand();
            }
        }

        public string OrganizationIdentificationNumber { get; }
        public string OrganizationTradingName { get; }
        public string OfficialJobCode { get; }
        public string OfficialExternalID { get; }
        public string FullName { get; }
        public bool IsActive { get; }
        public long? PartyRoleID { get; set; }
    }
}
