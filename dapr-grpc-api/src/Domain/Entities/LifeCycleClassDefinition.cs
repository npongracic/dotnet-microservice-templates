// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public partial class LifeCycleClassDefinition
    {
        public LifeCycleClassDefinition()
        {
            EntitySpecifications = new HashSet<EntitySpecification>();
            LifeCycleClassDefLifeCycleTrans = new HashSet<LifeCycleClassDefLifeCycleTran>();
            LifeCycles = new HashSet<LifeCycle>();
        }

        public int Id { get; set; }
        public string ClassName { get; set; }

        public virtual ICollection<EntitySpecification> EntitySpecifications { get; set; }
        public virtual ICollection<LifeCycleClassDefLifeCycleTran> LifeCycleClassDefLifeCycleTrans { get; set; }
        public virtual ICollection<LifeCycle> LifeCycles { get; set; }
    }
}