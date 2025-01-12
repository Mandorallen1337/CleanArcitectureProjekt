using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class DeleteJobbApplicationCommandHandler : IRequestHandler<DeleteJobbApplicationCommand, JobbApplication>
    {
        public Task<JobbApplication> Handle(DeleteJobbApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
