using Application.Commands.JobbApplicationCommands;
using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DeleteJobbApplicationHandler : IRequestHandler<DeleteJobbApplicationCommand, bool>
{
    private readonly IRepository<JobbApplicationViewModel> _jobbApplicationRepository;

    public DeleteJobbApplicationHandler(IRepository<JobbApplicationViewModel> jobbApplicationRepository)
    {
        _jobbApplicationRepository = jobbApplicationRepository;
    }

    public async Task<bool> Handle(DeleteJobbApplicationCommand request, CancellationToken cancellationToken)
    {
        var result = await _jobbApplicationRepository.DeleteByIdAsync(request.Id);
        return result != "Entity not found";
    }
}
