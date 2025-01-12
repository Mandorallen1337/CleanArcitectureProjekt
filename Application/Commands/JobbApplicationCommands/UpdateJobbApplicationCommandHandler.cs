using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class UpdateJobbApplicationCommandHandler : IRequestHandler<UpdateJobbApplicationCommand, JobbApplication>
    {
        public Task<JobbApplication> Handle(UpdateJobbApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
