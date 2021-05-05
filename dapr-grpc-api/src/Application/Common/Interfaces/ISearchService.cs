using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface ISearchService<T>
    {
        public Task<PageableCollection<T>> Search(string source, string searchTerm, QueryOptions queryOptions);
    }
}
