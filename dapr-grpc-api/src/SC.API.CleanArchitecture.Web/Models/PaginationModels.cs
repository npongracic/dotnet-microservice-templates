using System.Collections.Generic;

namespace SC.API.CleanArchitecture.API.Models
{
    public class PaginationModel<T>
    {
        public long Total { get; set; }
        public List<T> Data { get; set; }
    }
}
