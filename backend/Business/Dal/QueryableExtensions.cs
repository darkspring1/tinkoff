using System;
using System.Linq;

namespace Sb.Business.Dal
{
    public static class QueryableExtensions
    {
        public static T Random<T>(this IQueryable<T> queryable)
        {
            var randomNum = (new Random()).Next(0, queryable.Count());
            return queryable
                .OrderBy(r => Guid.NewGuid())
                .Skip(randomNum)
                .First();
        }
    }
}
