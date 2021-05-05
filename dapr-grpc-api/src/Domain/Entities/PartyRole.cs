﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class PartyRole
    {
        public PartyRole()
        {
            EntitySpecInvolvRoleTypeUses = new HashSet<EntitySpecInvolvRoleTypeUse>();
            InvolvementRoles = new HashSet<InvolvementRole>();
            PartyRoleAssociationPartyRoleInvolvedWiths = new HashSet<PartyRoleAssociation>();
            PartyRoleAssociationPartyRoleInvolves = new HashSet<PartyRoleAssociation>();
            PartyRoleContactableVia = new HashSet<PartyRoleContactableVia>();
        }

        public long Id { get; set; }
        public int? PartyId { get; set; }
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidDue { get; set; }
        public int PartyRoleTypeId { get; set; }
        public bool IsActive { get; set; }
        public string JobTitle { get; set; }
        public string JobCode { get; set; }

        public virtual Party Party { get; set; }
        public virtual PartyRoleType PartyRoleType { get; set; }
        public virtual ICollection<EntitySpecInvolvRoleTypeUse> EntitySpecInvolvRoleTypeUses { get; set; }
        public virtual ICollection<InvolvementRole> InvolvementRoles { get; set; }
        public virtual ICollection<PartyRoleAssociation> PartyRoleAssociationPartyRoleInvolvedWiths { get; set; }
        public virtual ICollection<PartyRoleAssociation> PartyRoleAssociationPartyRoleInvolves { get; set; }
        public virtual ICollection<PartyRoleContactableVia> PartyRoleContactableVia { get; set; }
    }
}