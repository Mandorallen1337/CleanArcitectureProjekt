using Application.Interfaces.AnalyzeInterface;
using Microsoft.Extensions.Configuration;
using OpenAI;

namespace Infrastructure.Services.AnalyzeService
{
    public class AnalyzeService : IAnalyze
    {
        private readonly OpenAIClient _openAIClient;
        private readonly string _apiKey;

        public AnalyzeService(IConfiguration configuration, OpenAIClient openAIClient)
        {
            var apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is missing.");
            }

            _apiKey = apiKey;
            _openAIClient = openAIClient;
        }

        public async Task<string> AnalyzeCVAsync(string fileContent)
        {
            throw new NotImplementedException();
        }
    }
}
