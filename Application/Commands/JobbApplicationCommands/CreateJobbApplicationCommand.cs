using MediatR;
using Domain.Models;

namespace Application.Commands.JobbApplicationCommands
{
    public class CreateJobbApplicationCommand : IRequest<JobbApplication>
    {
        public CreateJobbApplicationCommand(JobbApplication newJobbApplication)
        {
            NewJobbApplication = newJobbApplication;
        }

        public JobbApplication NewJobbApplication { get; }
    }
}
