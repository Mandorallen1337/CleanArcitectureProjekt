using Application.Interfaces.AnalyzeInterface;

namespace Infrastructure.Services.AnalyzeService
{
    public class AnalyzeService : IAnalyze
    {
        public Task<string> AnalyzeCVAsync(string blobFileName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
