using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Extensions
{
    public static class IncludingExtension
    {
        public static IQueryable<TSource> IncludeProperties<TSource>(this IQueryable<TSource> source, string? includeProperties) where TSource : class
        {
            if (!string.IsNullOrEmpty(includeProperties) && !string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (',', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrEmpty(includeProperty))
                    {
                        source = source.Include(includeProperty);
                    }
                }
            }

            return source;
        }
    }
}
