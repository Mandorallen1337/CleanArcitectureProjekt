using Application.Interfaces.OpenAiInterface;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
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
            var requestbody = new
            {
                model = "gpt-3.5-turbo",
                prompt = text,
                max_tokens = 500,
                temperature = 0.5
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestbody), Encoding.UTF8, "application/json");

            // Lägg till API-nyckel i headers
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            // Skicka POST-anrop till OpenAI API
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            // Logga responsen för debugging
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("OpenAI Response: " + responseContent);

            // Hantera fel vid misslyckad förfrågan
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenAI API request failed. Status: {response.StatusCode}, Response: {responseContent}");
            }

            // Deserialisera API-svaret
            var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<JsonDocument>(responseContent);

            if (!jsonResponse.RootElement.TryGetProperty("choices", out var choices) || choices.GetArrayLength() == 0)
            {
                throw new Exception("OpenAI API response is missing 'choices'. Response: " + responseContent);
            }

            var summary = choices[0].GetProperty("text").GetString();

            return new AnalysisResult
            {
                Summary = summary
            };
        }
    }
}
