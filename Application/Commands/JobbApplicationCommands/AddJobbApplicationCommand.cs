using MediatR;
using Domain.Models;

namespace Application.Commands.JobbApplicationCommands
{
    public class AddJobbApplicationCommand : IRequest<JobbApplication>
    {
        public AddJobbApplicationCommand(JobbApplication newJobbApplication)
        {
            NewJobbApplication = newJobbApplication;
        }

        public JobbApplication NewJobbApplication { get; }
    }
}
