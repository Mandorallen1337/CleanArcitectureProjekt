using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.AnalyzeInterface
{
    public interface IAnalyze
    {
        Task<string> AnalyzeCVAsync(IFormFile file);
    }
}
