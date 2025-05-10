using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
        public string? Token { get; private set; }
        public Guid? RefreshToken { get; private set; }
        public DateTimeOffset? RefreshTokenExpiryDate { get; private set; }
        public void SetToken(string newToken)
        {
            Token = newToken;
        }
        public void SetRefreshToken(Guid newToken)
        {
            RefreshToken = newToken;
        }
        public void SetRefreshTokenExpiryDate(DateTimeOffset newExpiryDate)
        {
            RefreshTokenExpiryDate = newExpiryDate;
        }
    }
}
