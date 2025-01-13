using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class DeleteJobbApplicationByIdCommandHandler : IRequestHandler<DeleteJobbApplicationByIdCommand, JobbApplication>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public DeleteJobbApplicationByIdCommandHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<JobbApplication> Handle(DeleteJobbApplicationByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var jobbApplicationToDelete = await _jobbApplicationRepository.GetByIdAsync(request.Id, cancellationToken);
                if (jobbApplicationToDelete == null)
                {
                    throw new KeyNotFoundException("JobbApplication not found with the provided ID.");
                }

                await _jobbApplicationRepository.DeleteByIdAsync(request.Id);
                return jobbApplicationToDelete;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the JobbApplication.", ex);
            }
        }
    }
}
