
using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.JobbApplicationQuerys;

public class GetAllJobbAplicationsHandler : IRequestHandler<GetAllJobbApplicationsQuery, List<JobbApplicationViewModel>>
{
    private readonly IRepository<JobbApplicationViewModel> _jobbApplicationRepository;

    public GetAllJobbAplicationsHandler(IRepository<JobbApplicationViewModel> jobbApplicationRepository)
    {
        _jobbApplicationRepository = jobbApplicationRepository;
    }

    public async Task<List<JobbApplicationViewModel>> Handle(GetAllJobbApplicationsQuery request, CancellationToken cancellationToken)
    {
        // Hämta alla jobbansökningar från databasen
        var jobApplications = await _jobbApplicationRepository.GetAllAsync() ?? new List<JobbApplicationViewModel>();        

        return jobApplications;
    }
}
