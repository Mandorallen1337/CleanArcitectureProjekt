using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class DeleteCVByIdCommandHandler : IRequestHandler<DeleteCVByIdCommand, CV>
    {
        public Task<CV> Handle(DeleteCVByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
