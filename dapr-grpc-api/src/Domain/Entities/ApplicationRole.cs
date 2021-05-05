using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public class ApplicationRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
