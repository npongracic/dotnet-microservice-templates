﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class EntitySpecInvolvRoleTypeUse
    {
        public int Id { get; set; }
        public int EntitySpecInvolvementRoleTypeId { get; set; }
        public long PartyRoleId { get; set; }
        public int? PartyRoleAssociationTypeId { get; set; }

        public virtual EntitySpecInvolvementRoleType EntitySpecInvolvementRoleType { get; set; }
        public virtual PartyRole PartyRole { get; set; }
        public virtual PartyRoleAssociationType PartyRoleAssociationType { get; set; }
    }
}