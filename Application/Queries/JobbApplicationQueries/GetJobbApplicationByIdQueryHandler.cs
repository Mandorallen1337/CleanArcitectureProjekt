using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Queries.JobbApplicationQueries
{
    public class GetJobbApplicationByIdQueryHandler : IRequestHandler<GetJobbApplicationByIdQuery, JobbApplication>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public GetJobbApplicationByIdQueryHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<JobbApplication> Handle(GetJobbApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var jobbApplication = await _jobbApplicationRepository.GetByIdAsync(request.Id, cancellationToken);
            if (jobbApplication == null)
            {
                throw new KeyNotFoundException("JobbApplication not found with the provided ID.");
            }

            return jobbApplication;
        }
    }
}
