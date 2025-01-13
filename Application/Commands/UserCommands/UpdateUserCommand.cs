using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Models;

namespace Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<IdentityResult>
    {
        public UpdateUserCommand(User updatedUser)
        {
            UpdatedUser = updatedUser;
        }

        public User UpdatedUser { get; }
    }
}
