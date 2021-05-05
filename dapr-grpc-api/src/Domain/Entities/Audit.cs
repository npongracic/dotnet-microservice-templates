using System;
using System.Collections.Generic;
using System.Text;

namespace SC.API.CleanArchitecture.Domain.Entities
{
    public class Audit
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string RecordedByParty { get; set; }
    }

}
