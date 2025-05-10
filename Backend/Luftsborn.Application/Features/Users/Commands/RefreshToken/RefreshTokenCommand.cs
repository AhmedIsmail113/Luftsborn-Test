using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application.Features.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<Response<UserTokenDto>>
    {
        public RefreshTokenCommand(string token, Guid refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
