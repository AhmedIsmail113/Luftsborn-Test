using Luftsborn.Dtos.Common;
using Luftsborn.Dtos.Entities.User;
using MediatR;


namespace Luftsborn.Application.Features.Users.Commands.Login
{
    public class LoginCommand : IRequest<Response<UserTokenDto>>
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
