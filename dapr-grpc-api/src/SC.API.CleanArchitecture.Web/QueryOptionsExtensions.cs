using SC.API.CleanArchitecture.Application.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.API
{
    public static class QueryOptionsExtensions
    {
        public static QueryOptions GetFromRequest(HttpRequest httpRequest)
        {
            var queryOptions = new QueryOptions() {
                Page = 1,
                Take = 25
            };

            var qSkip = httpRequest.Query["skip"];

            if (qSkip.Count > 0) {
                queryOptions.Page = Convert.ToInt32(qSkip);
            }

            var qTake = httpRequest.Query["take"];

            if (qTake.Count > 0) {
                queryOptions.Take = Convert.ToInt32(qTake);
            }

            if (queryOptions.Take > 100) {
                throw new InvalidOperationException("Tak must be less or equal to 100");
            }

            var index = 0;

            var sort = new List<QueryOptionsByDesc>();

            while (httpRequest.Query[$"sortBy[{index}]"].Count > 0) {
                var sortDesc = httpRequest.Query[$"sortDesc[{index}]"];

                sort.Add(new QueryOptionsByDesc(httpRequest.Query[$"sortBy[{index}]"].ToString(), sortDesc.Count > 0 ? Convert.ToBoolean(sortDesc) : false));

                index++;
            }

            queryOptions.Sort = sort;

            index = 0;

            var group = new List<QueryOptionsByDesc>();

            while (httpRequest.Query[$"groupBy[{index}]"].Count > 0) {
                var groupDesc = httpRequest.Query[$"groupDesc[{index}]"];

                group.Add(new QueryOptionsByDesc(httpRequest.Query[$"groupBy[{index}]"].ToString(), groupDesc.Count > 0 ? Convert.ToBoolean(groupDesc) : false));

                index++;
            }

            queryOptions.Group = group;

            return queryOptions;
        }
    }
}
