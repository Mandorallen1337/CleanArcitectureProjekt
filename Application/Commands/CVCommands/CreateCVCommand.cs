using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class CreateCVCommand : IRequest<CV>
    {
        public CreateCVCommand(CV newCV)
        {
            NewCV = newCV;
        }
        public CV NewCV { get; }
    }
}
