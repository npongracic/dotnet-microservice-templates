﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class PermissionLevel
    {
        public PermissionLevel()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}