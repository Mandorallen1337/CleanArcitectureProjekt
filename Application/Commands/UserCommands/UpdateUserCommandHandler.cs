using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        public Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
