using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommandHandler : IRequestHandler<UpdateCVByIdCommand, CV>
    {
        public Task<CV> Handle(UpdateCVByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
