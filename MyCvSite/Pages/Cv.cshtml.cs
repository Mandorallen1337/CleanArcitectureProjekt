using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyCvSite.Pages
{
    public class CvModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CvModel(IConfiguration configuration, HttpClient httpClient)
        {
            var apiBaseUrl = configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new Exception("API base URL is not configured.");
            }

            _apiBaseUrl = apiBaseUrl;
            _httpClient = httpClient;
        }

        public List<CV> CVs { get; set; } = new();

        public async Task OnGet()
        {
            try
            {
                var existingCVs = await _httpClient.GetFromJsonAsync<List<CV>>($"{_apiBaseUrl}/cv");
                CVs = existingCVs ?? new List<CV>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching CVs: {ex.Message}");
                CVs = new List<CV>();
            }
        }
    }
}

