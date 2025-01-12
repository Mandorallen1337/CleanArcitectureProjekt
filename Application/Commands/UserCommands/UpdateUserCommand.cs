using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<User>
    {
    }
}
