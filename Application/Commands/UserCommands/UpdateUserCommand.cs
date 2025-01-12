using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<User>
    {
        public UpdateUserCommand(User updatedUser, Guid id)
        {
            UpdatedUser = updatedUser;
            Id = id;
        }

        public User UpdatedUser { get; }
        public Guid Id { get; }
    }
}
