using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Common
{
    public class BaseQueryRequest
    {
        public BaseQueryRequest(string? orderBy = null, bool isAcending = true)
        {
            OrderBy = orderBy;
            IsAcending = isAcending;
        }
        public string? OrderBy { get; set; }
        public bool IsAcending { get; set; }
    }
}
