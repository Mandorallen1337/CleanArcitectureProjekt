using Application.Interfaces.BlobStorageInterface;
using Application.Interfaces.OpenAiInterface;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CVCommands
{
    public class AnalyzeCvCommandHandler : IRequestHandler<AnalyzeCvCommand, AnalysisResult>
    {
        //private readonly IBlobStorage _blobStorage;
        private readonly IOpenAiService _openAiService;
        private readonly ILogger<AnalyzeCvCommandHandler> _logger;

        public AnalyzeCvCommandHandler(IOpenAiService openAiService)
        {
            
            _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
            
        }
        public async Task<AnalysisResult> Handle(AnalyzeCvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Extract text from the PDF
                string cvText = await _openAiService.ExtractTextFromPdf(request.CvFile);
                
                // Call the OpenAI service to analyze the extracted text
                var analysisResult = await _openAiService.AnalyzeTextAsync(cvText);
                // Check if the analysis result is valid
                if (analysisResult == null || string.IsNullOrEmpty(analysisResult.Summary))
                {
                    _logger.LogWarning("Analysis result is null or summary is empty");

                    // Return a default AnalysisResult with warning message
                    return new AnalysisResult
                    {
                        Summary = "Unable to extract useful information from CV",
                        KeySkills = new List<string>(),
                        ExperienceDetails = "Unable to extract useful information from CV"
                    };
                }
                // Return the actual analysis result if valid
                return analysisResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while analyzing the CV.");
                throw;
            }
            

            
            

            

            
        }

    }
}
