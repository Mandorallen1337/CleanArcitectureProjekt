using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobbapplication.JobbApplicationQuerys.GetAllJobbAplicationsQuery
{
    public class GetAllJobbApplicationsHandler : IRequestHandler<GetAllJobbApplicationsQuery, IEnumerable<JobbApplication>>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public GetAllJobbApplicationsHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<IEnumerable<JobbApplication>> Handle(GetAllJobbApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await _jobbApplicationRepository.GetAllAsync();
        }
    }

}
