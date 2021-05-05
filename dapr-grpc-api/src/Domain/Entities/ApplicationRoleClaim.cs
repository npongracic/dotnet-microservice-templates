using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public class ApplicationRoleClaim
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
