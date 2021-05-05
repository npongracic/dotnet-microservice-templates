using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public class ApplicationUserRole
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
