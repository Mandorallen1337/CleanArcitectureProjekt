using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class UpdateJobbApplicationCommand : IRequest<JobbApplication>
    {
        public UpdateJobbApplicationCommand(JobbApplication updatedJobbApplication, Guid id)
        {
            UpdatedJobbApplication = updatedJobbApplication;
            Id = id;
        }

        public JobbApplication UpdatedJobbApplication { get; }
        public Guid Id { get; }
    }
}
