using Luftsborn.Dtos.Common;
using MediatR;

namespace Luftsborn.Application.Features.Users.Commands.Register
{
    public class RegisterCommand : IRequest<Response<Guid>>
    {
        public RegisterCommand(string firstName, string lastName, string password, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
