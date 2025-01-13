using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class UpdateJobbApplicationByIdCommandHandler : IRequestHandler<UpdateJobbApplicationByIdCommand, JobbApplication>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public UpdateJobbApplicationByIdCommandHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<JobbApplication> Handle(UpdateJobbApplicationByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdatedJobbApplication == null)
            {
                throw new ArgumentNullException(nameof(request.UpdatedJobbApplication), "The updated JobbApplication data must not be null.");
            }

            try
            {
                var existingJobbApplication = await _jobbApplicationRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingJobbApplication == null)
                {
                    throw new KeyNotFoundException("JobbApplication not found with the provided ID.");
                }

                request.UpdatedJobbApplication.Id = request.Id;
                await _jobbApplicationRepository.UpdateAsync(request.UpdatedJobbApplication, cancellationToken);
                return request.UpdatedJobbApplication;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the JobbApplication.", ex);
            }
        }
    }
}
