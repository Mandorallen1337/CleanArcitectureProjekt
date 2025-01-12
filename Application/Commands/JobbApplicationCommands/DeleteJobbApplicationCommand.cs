using Domain.Models;
using MediatR;

namespace Application.Commands.JobbApplicationCommands
{
    public class DeleteJobbApplicationCommand : IRequest<JobbApplication>
    {
    }
}
