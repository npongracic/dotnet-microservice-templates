﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class PartyRoleAssociationType
    {
        public PartyRoleAssociationType()
        {
            EntitySpecInvolvRoleTypeUses = new HashSet<EntitySpecInvolvRoleTypeUse>();
            PartyRoleAssociations = new HashSet<PartyRoleAssociation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EntitySpecInvolvRoleTypeUse> EntitySpecInvolvRoleTypeUses { get; set; }
        public virtual ICollection<PartyRoleAssociation> PartyRoleAssociations { get; set; }
    }
}