using GitHubTask.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubTask.Utils
{
    public static class GitHubUtils
    {
        public static async Task<IEnumerable<T>> GetAllAsync<T>(Func<PaginationQuery, Task<IEnumerable<T>>> action)
        {
            var query = new PaginationQuery() { Page = 1, PerPage = 30 };
            var allItems = new List<T>();
            while (true)
            {
                var result = await action(query);
                allItems.AddRange(result);
                if (result.Count() < query.PerPage)
                    return new ReadOnlyCollection<T>(allItems);
                query.Page++;
            }
        }
    }
}
