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
        public IFormFile CvFile { get; }
        public AnalyzeCvCommand(IFormFile cvFile)
        {
            CvFile = cvFile ?? throw new ArgumentNullException(nameof(cvFile));
        }
    }
}
