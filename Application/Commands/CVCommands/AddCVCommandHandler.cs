using MediatR;
using Domain.Models;

namespace Application.Commands.CVCommands
{
    public class AddCVCommandHandler : IRequestHandler<AddCVCommand, CV>
    {
        Task<CV> IRequestHandler<AddCVCommand, CV>.Handle(AddCVCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
