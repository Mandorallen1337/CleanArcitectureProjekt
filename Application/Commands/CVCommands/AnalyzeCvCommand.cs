using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CVCommands
{
    public class AnalyzeCvCommand : IRequest<AnalysisResult>
    {
        public string FileUrl { get; set; } = string.Empty;
    }
}
