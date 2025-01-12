using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        public Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
