using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Common
{
    public interface IAuditable
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? ChangedByPartyId { get; set; }
    }
}
