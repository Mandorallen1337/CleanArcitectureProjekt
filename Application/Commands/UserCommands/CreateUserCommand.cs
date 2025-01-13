using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public CreateUserCommand(User newUser, string password)
        {
            NewUser = newUser;
            Password = password;
        }
        public User NewUser { get; }
        public string Password { get; }
    }
}
