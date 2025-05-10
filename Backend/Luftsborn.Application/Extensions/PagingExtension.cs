using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Extensions
{
    public static class PagingExtension
    {
        public static IQueryable<TSource> Paging<TSource>(this IQueryable<TSource> source, int? pageNumber = null, int? itemsPerPage = null)
        {
            IQueryable<TSource> result;
            if (pageNumber > 0 && itemsPerPage > 0)
            {
                result = source
                    .Skip((pageNumber.Value - 1) * itemsPerPage.Value)
                    .Take(itemsPerPage.Value);
            }
            else
            {
                result = source;
            }
            return result;
        }
    }
}
