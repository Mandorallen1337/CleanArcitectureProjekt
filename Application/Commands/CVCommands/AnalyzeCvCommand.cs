using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
