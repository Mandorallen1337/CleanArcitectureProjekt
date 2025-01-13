using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class CreateJobbApplicationCommandHandler : IRequestHandler<CreateJobbApplicationCommand, JobbApplication>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public CreateJobbApplicationCommandHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<JobbApplication> Handle(CreateJobbApplicationCommand request, CancellationToken cancellationToken)
        {
            if (request.NewJobbApplication == null)
            {
                throw new ArgumentNullException(nameof(request.NewJobbApplication), "The JobbApplication data must not be null.");
            }

            try
            {
                var addedJobbApplication = await _jobbApplicationRepository.CreateAsync(request.NewJobbApplication);
                return addedJobbApplication;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the JobbApplication.", ex);
            }
        }
    }
}
