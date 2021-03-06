// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class EntityOperationLog
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public int OperationId { get; set; }
        public DateTime Timestamp { get; set; }
        public string OldObject { get; set; }
        public string NewObject { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual Operation Operation { get; set; }
    }
}