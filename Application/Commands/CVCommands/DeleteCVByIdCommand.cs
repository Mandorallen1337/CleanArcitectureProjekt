using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class DeleteCVByIdCommand : IRequest<CV>
    {
        public DeleteCVByIdCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
    
}
