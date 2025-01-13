using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class UpdateJobbApplicationByIdCommand : IRequest<JobbApplication>
    {
        public UpdateJobbApplicationByIdCommand(JobbApplication updatedJobbApplication, Guid id)
        {
            UpdatedJobbApplication = updatedJobbApplication;
            Id = id;
        }

        public JobbApplication UpdatedJobbApplication { get; }
        public Guid Id { get; }
    }
}
