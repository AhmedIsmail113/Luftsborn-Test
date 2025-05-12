using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Extensions
{
    public static class IHttpContextAccessorExtension
    {
        public static Guid? GetCurrentUserId(this IHttpContextAccessor httpContextAccessor)
        {
            if (Guid.TryParse(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid _userId))
            {
                return _userId;
            }
            else
            {
                return null;
            }
        }
    }
}
