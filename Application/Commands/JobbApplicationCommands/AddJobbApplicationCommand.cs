using MediatR;
using Domain.Models;

namespace Application.Commands.JobbApplicationCommands
{
    public class AddJobbApplicationCommand : IRequest<JobbApplication>
    {
    }
}
