using Domain.Models;
using MediatR;

namespace Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<User>
    {
    }
}
