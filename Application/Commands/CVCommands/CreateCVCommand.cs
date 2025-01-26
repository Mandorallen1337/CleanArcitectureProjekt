using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.CVCommands
{
    public class CreateCVCommand : IRequest<CV>
    {
        public CreateCVCommand(IFormFile newCV, Guid userId)
        {
            NewCV = newCV;
            UserId = userId;
            FileName = Path.GetFileName(newCV.FileName);
        }
        public IFormFile NewCV { get; }
        public Guid UserId { get; }
        public string FileName { get; }
    }
}
