using Application.Utilities.DownloadFile;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class DownloadCVByIdCommand : IRequest<FileResult>
    {
        public Guid Id { get; }

        public DownloadCVByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
