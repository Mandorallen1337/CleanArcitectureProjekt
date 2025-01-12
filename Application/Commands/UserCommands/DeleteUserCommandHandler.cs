using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
    {
        public Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
