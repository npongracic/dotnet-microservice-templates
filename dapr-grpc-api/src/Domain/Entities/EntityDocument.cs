﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class EntityDocument
    {
        public long DocumentId { get; set; }
        public long EntityId { get; set; }

        public virtual Document Document { get; set; }
        public virtual Entity Entity { get; set; }
    }
}