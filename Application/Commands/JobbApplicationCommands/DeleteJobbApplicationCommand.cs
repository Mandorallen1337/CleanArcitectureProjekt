using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class DeleteJobbApplicationCommand : IRequest<JobbApplication>
    {
        public DeleteJobbApplicationCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
