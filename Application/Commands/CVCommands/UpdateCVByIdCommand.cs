using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommand : IRequest<CV>
    {
        public UpdateCVByIdCommand(CV updatedCV, Guid id)
        {
            UpdatedCV = updatedCV;
            Id = id;
        }

        public CV UpdatedCV { get; }
        public Guid Id { get; }
    }
}
