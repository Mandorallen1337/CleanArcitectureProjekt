using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class AddJobbApplicationCommandHandler : IRequestHandler<AddJobbApplicationCommand, JobbApplication>
    {
        public Task<JobbApplication> Handle(AddJobbApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
