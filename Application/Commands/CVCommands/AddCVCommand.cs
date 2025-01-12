using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class AddCVCommand : IRequest<CV>
    {
        public AddCVCommand(CV newCV)
        {
            NewCV = newCV;
        }

        public CV NewCV { get; }
    }
}
