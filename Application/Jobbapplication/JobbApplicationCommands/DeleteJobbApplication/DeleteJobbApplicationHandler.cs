using Application.Interfaces.RepoInterface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;  

namespace Application.Jobbapplication.JobbApplicationCommands.DeleteJobbApplication
{
    public class DeleteJobbApplicationHandler : IRequestHandler<DeleteJobbApplicationCommand, string>
    {
        private readonly IRepository<JobbApplication> _jobbApplicationRepository;

        public DeleteJobbApplicationHandler(IRepository<JobbApplication> jobbApplicationRepository)
        {
            _jobbApplicationRepository = jobbApplicationRepository;
        }

        public async Task<string> Handle(DeleteJobbApplicationCommand request, CancellationToken cancellationToken)
        {
            var result = await _jobbApplicationRepository.DeleteByIdAsync(request.Id);
            return result ?? "Entity not found";
        }
    }

}
