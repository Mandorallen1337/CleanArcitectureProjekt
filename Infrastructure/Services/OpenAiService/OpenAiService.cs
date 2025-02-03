using Application.Interfaces.BlobStorageInterface;
using Application.Interfaces.OpenAiInterface;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UglyToad.PdfPig;

namespace Infrastructure.Services.OpenAiService
{
    public class OpenAiService : IOpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = _apiKey = configuration["OpenAI:ApiKey"];

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
                    new { role = "system",
                    content = "You are a professional career coach and recruiter specializing in CV reviews.\nYou will analyze resumes and provide detailed feedback to improve the document."},
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

            return new AnalysisResult
            {
                Summary = summary
            };
        }

        public Task<string> ExtractTextFromPdf(Stream pdfStream)
        {
            if (pdfStream == null || pdfStream.Length == 0)
            {
                throw new ArgumentNullException("No file uploaded.");
            }

            using var pdfDocument = PdfDocument.Open(pdfStream);
            var text = new StringBuilder();

            foreach (var page in pdfDocument.GetPages())
            {
                text.Append(page.Text);
            }

            return Task.FromResult(text.ToString());
        }
    }
}
