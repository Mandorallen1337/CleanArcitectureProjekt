using Application.Interfaces.BlobStorageInterface;
using Application.Interfaces.OpenAiInterface;
using Domain.Models;
using Infrastructure.Services.BlobStorageService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UglyToad.PdfPig;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IBlobStorage _blobStorageService;

        public OpenAiService(HttpClient httpClient, IConfiguration configuration, IBlobStorage blobStorageService)
        {
            _httpClient = httpClient;
            _apiKey = _apiKey = configuration["OpenAI:ApiKey"];
            _blobStorageService = blobStorageService;

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new ArgumentNullException("OpenAI API Key is missing");
            }

        }              
        

        public async Task<AnalysisResult> AnalyzeTextAsync(string text)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are an AI assistant that analyzes CVs and extracts useful information." },
                    new { role = "user", content = text }
                },
                max_tokens = 500,
                temperature = 0.5
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenAI API request failed. Status: {response.StatusCode}, Response: {responseContent}");
            }

            var jsonResponse = JObject.Parse(responseContent);
            var summary = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();

            if (string.IsNullOrEmpty(summary))
            {
                throw new Exception("OpenAI API response did not contain expected content.");
            }
            
            // Return only the summary, without further skill and experience analysis.
            return new AnalysisResult
            {
                Summary = summary                
            };
        }

        public Task<string> ExtractTextFromPdf(IFormFile cvFile)
        {
            if (cvFile == null || cvFile.Length == 0)
            {
                throw new ArgumentNullException("No file uploaded.");
            }
            using var pdfStream = cvFile.OpenReadStream();

            using var pdfDocument = PdfDocument.Open(pdfStream);
            var text = new StringBuilder();
            foreach (var page in pdfDocument.GetPages())
            {
                text.Append(page.Text);
            }
            return Task.FromResult(text.ToString());
        }

        private async Task<(List<string> Skills, string Experience)> AnalyzeSkillsAndExperience(string summary)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "analys cv and give feedback" },
                new { role = "user", content = summary }
            },
                max_tokens = 1000,
                temperature = 0.7
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return (new List<string>(), "No experience data available.");
            }

            var jsonResponse = JObject.Parse(responseContent);
            var extractedText = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();

            if (string.IsNullOrEmpty(extractedText))
            {
                return (new List<string>(), "No experience data available.");
            }

            var skills = extractedText.Contains("Skills:")
                ? extractedText.Split("Skills:")[1].Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList()
                : new List<string>();

            var experience = extractedText.Contains("Experience:")
                ? extractedText.Split("Experience:")[1].Trim()
                : "No experience data available.";

            return (skills, experience);
        }

        

        


    }
}
