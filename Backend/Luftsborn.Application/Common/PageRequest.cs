using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Common
{
    public class PageRequest : BaseQueryRequest
    {
        public PageRequest(int? pageNumber = 1, int? pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
