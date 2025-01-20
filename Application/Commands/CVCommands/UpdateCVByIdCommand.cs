using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommand : IRequest<CV>
    {
        public UpdateCVByIdCommand(Guid id, CV updatedCV)
        {
            Id = id;
            UpdatedCV = updatedCV;
        }

        public Guid Id { get; set; }
        public CV UpdatedCV { get; set; }
    }
}
