﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class PermissionTemplate
    {
        public PermissionTemplate()
        {
            PermissionTemplatePermissions = new HashSet<PermissionTemplatePermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PermissionTemplatePermission> PermissionTemplatePermissions { get; set; }
    }
}