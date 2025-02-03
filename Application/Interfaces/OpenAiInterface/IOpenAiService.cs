using Domain.Models;

namespace Application.Interfaces.OpenAiInterface
{
    public interface IOpenAiService
    {        
        Task<AnalysisResult> AnalyzeTextAsync(string text);
        Task<string> ExtractTextFromPdf(Stream stream);
    }
}
