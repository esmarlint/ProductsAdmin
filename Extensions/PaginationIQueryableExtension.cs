using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Extensions
{
    public static class PaginationIQueryableExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page, int pageSize)
        {
            if (source == null)
            {
                throw new ArgumentException($"Parameter '{nameof(source)}' cannot be null");
            }

            source = source
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return source;
        }
    }
}
