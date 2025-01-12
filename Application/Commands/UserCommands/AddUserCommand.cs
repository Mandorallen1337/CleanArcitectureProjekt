using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<User>
    {
    }
}
