using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class AnalyzeCvCommand : IRequest<AnalysisResult>
    {
        public Guid CvId { get; }
        public AnalyzeCvCommand(Guid cvId)
        {
            CvId = cvId;
        }
    }
}
