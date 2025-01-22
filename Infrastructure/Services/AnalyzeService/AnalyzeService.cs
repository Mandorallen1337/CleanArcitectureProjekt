using Application.Interfaces.AnalyzeInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.AnalyzeService
{
    public class AnalyzeService : IAnalyze
    {
        public AnalyzeService(IConfiguration configuration)
        {
            
        }
        public async Task<string> AnalyzeCVAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
