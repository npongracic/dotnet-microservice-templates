﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class InvolvementRoleType
    {
        public InvolvementRoleType()
        {
            EntitySpecInvolvementRoleTypes = new HashSet<EntitySpecInvolvementRoleType>();
            InvolvementRoleTypePartyRoleTypeInvolves = new HashSet<InvolvementRoleTypePartyRoleTypeInvolf>();
            InvolvementRoles = new HashSet<InvolvementRole>();
            OperationInvolvementRoleTypes = new HashSet<OperationInvolvementRoleType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EntitySpecInvolvementRoleType> EntitySpecInvolvementRoleTypes { get; set; }
        public virtual ICollection<InvolvementRoleTypePartyRoleTypeInvolf> InvolvementRoleTypePartyRoleTypeInvolves { get; set; }
        public virtual ICollection<InvolvementRole> InvolvementRoles { get; set; }
        public virtual ICollection<OperationInvolvementRoleType> OperationInvolvementRoleTypes { get; set; }
    }
}