using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<User>
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
