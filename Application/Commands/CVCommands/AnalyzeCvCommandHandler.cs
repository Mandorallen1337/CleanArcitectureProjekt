using Application.Commands.CVCommands;
using Application.Interfaces.BlobStorageInterface;
using Application.Interfaces.OpenAiInterface;
using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

public class AnalyzeCvCommandHandler : IRequestHandler<AnalyzeCvCommand, AnalysisResult>
{
    private readonly IRepository<CV> _cvRepository;
    private readonly IBlobStorage _blobStorage;
    private readonly IOpenAiService _openAiService;
    private readonly ILogger<AnalyzeCvCommandHandler> _logger;

    public AnalyzeCvCommandHandler(
        IRepository<CV> cvRepository,
        IBlobStorage blobStorage,
        IOpenAiService openAiService,
        ILogger<AnalyzeCvCommandHandler> logger)
    {
        _cvRepository = cvRepository;
        _blobStorage = blobStorage;
        _openAiService = openAiService;
        _logger = logger;
    }

    public async Task<AnalysisResult> Handle(AnalyzeCvCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 🔹 Step 1: Fetch CV from Database
            var cv = await _cvRepository.GetByIdAsync(request.CvId, cancellationToken);
            if (cv == null || string.IsNullOrEmpty(cv.FileUrl))
            {
                _logger.LogWarning("CV with ID {CvId} was not found.", request.CvId);
                throw new KeyNotFoundException($"CV with ID {request.CvId} was not found.");
            }

            // 🔹 Step 2: Get File from Azure Blob Storage
            string blobFileName = cv.FileUrl;
            using var stream = await _blobStorage.DownloadFileAsync(blobFileName);
            
            //var cvText = _openAiService.ExtractTextFromPdf(stream);


            // 🔹 Step 3: Send Text to OpenAI for Analysis
            string cvText = await _openAiService.ExtractTextFromPdf(stream);

            if (cvText == null || string.IsNullOrEmpty(cvText))
            {
                _logger.LogWarning("Analysis result is null or summary is empty.");
                return new AnalysisResult
                {
                    Summary = "Unable to extract useful information from CV",
                    KeySkills = new List<string>(),
                    ExperienceDetails = "No experience details found"
                };
            }

            return await _openAiService.AnalyzeTextAsync(cvText);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while analyzing the CV.");
            throw;
        }
    }
}

