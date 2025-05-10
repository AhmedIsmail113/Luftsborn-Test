using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Dtos.Entities.User
{
    public class UserTokenDto
    {
        public Guid? Id { get; set; }
        public string? Token { get; set; }
        public Guid? RefreshToken { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
    }
}
