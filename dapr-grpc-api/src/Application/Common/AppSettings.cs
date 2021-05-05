using SC.API.CleanArchitecture.Application.Common.Interfaces;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Application.Common
{


    public class AppSettings 
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SendEmail { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpLocalDomain { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public IEnumerable<string> MimeTypes { get; set; }
        public string DomainPrefix { get; set; } = "INTRANET";
        public string DefaultCurrency { get; set; } = "HRK";
        public bool DisableUserPermissionCheck { get; set; } = false;
      
    }
}
