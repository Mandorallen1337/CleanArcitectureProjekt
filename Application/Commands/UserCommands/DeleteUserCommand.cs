using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Models;

namespace Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<IdentityResult>
    {
        public DeleteUserCommand(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}