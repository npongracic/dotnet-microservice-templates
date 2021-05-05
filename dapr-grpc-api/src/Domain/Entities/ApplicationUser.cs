using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        public ApplicationUser()
        {
            EntityOperationLogs = new HashSet<EntityOperationLog>();
            EntityProcessLogs = new HashSet<EntityProcessLog>();
            UserRoles = new HashSet<ApplicationUserRole>();
        }


        public virtual ICollection<EntityOperationLog> EntityOperationLogs { get; set; }
        public virtual ICollection<EntityProcessLog> EntityProcessLogs { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
