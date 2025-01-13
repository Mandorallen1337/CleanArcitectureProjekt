using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Queries.JobbApplicationQueries
{
    public class GetAllJobbApplicationsQueryHandler : IRequestHandler<GetAllJobbApplicationsQuery, List<JobbApplication>>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public GetAllJobbApplicationsQueryHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<List<JobbApplication>> Handle(GetAllJobbApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await _jobbApplicationRepository.GetAllAsync();
        }
    }
}
