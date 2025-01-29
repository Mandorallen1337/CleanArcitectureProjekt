using Application.Commands.JobbApplicationCommands;
using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class UpdateJobbApplicationHandler : IRequestHandler<UpdateJobbApplicationCommand, Unit>
{
    private readonly IRepository<JobbApplicationViewModel> _jobbApplicationRepository;

    public UpdateJobbApplicationHandler(IRepository<JobbApplicationViewModel> jobbApplicationRepository)
    {
        _jobbApplicationRepository = jobbApplicationRepository;
    }

    public async Task<Unit> Handle(UpdateJobbApplicationCommand request, CancellationToken cancellationToken)
    {
        // Hämta befintlig jobbansökan från databasen
        var jobbApplication = await _jobbApplicationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (jobbApplication == null)
        {
            throw new KeyNotFoundException($"Jobbansökan med ID {request.Id} hittades inte.");
        }

        // Uppdatera fälten
        jobbApplication.JobTitle = request.JobTitle;
        jobbApplication.CompanyName = request.CompanyName;
        jobbApplication.Status = request.Status;
        jobbApplication.ApplicationDate = request.ApplicationDate;

        // Uppdatera jobbansökan i databasen
        await _jobbApplicationRepository.UpdateAsync(jobbApplication, cancellationToken);

        // Returnera enhet (Unit.Value)
        return Unit.Value;
    }
}
