using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Application.Common
{
    public class QueryOptionsByDesc
    {
        public QueryOptionsByDesc()
        {

        }

        public QueryOptionsByDesc(string by, bool descending)
        {
            By = by;
            Descending = descending;
        }

        public string By { get; set; }
        public bool Descending { get; set; }
    }

    public class QueryOptions
    {
        public QueryOptions()
        {
            Sort = new List<QueryOptionsByDesc>();
            Group = new List<QueryOptionsByDesc>();
        }
        public string Value { get; set; }
        public int Page { get; set; } = 1;
        //public int? Skip { get; set; } = 0;
        public int? Take { get; set; }
        public IEnumerable<QueryOptionsByDesc> Sort { get; set; }
        public IEnumerable<QueryOptionsByDesc> Group { get; set; }
    }
}
