using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class DeleteJobbApplicationByIdCommand : IRequest<JobbApplication>
    {
        public DeleteJobbApplicationByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
