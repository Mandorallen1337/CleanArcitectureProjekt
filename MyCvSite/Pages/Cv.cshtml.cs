using Application.Interfaces.OpenAiInterface;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCvSite.Controllers;
using System.Diagnostics;
using System.Security.Claims;

namespace MyCvSite.Pages
{
    [Authorize]
    public class CvModel : PageModel
    {
        private readonly CVController _cvController;
        private readonly IOpenAiService _openAiService;

        public CvModel(CVController cvController, IOpenAiService openAiService)
        {
            _cvController = cvController;
            _openAiService = openAiService;
        }

        public List<CV> CVs { get; set; } = new();
        public AnalysisResult Analysis { get; set; }

        public async Task OnGet()
        {
            try
            {
                await LoadCvsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching CVs: {ex.Message}");
                CVs = new List<CV>();
            }
        }


        public async Task<IActionResult> OnPostCreate(IFormFile cv)
        {
            if (cv == null || cv.Length == 0)
            {
                TempData["Error"] = "Please select a valid PDF file.";
                return RedirectToPage();
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    TempData["Error"] = "Unable to get user Id.";
                    return RedirectToPage();
                }

                var result = await _cvController.Create(cv, Guid.Parse(userId));

                if (result is OkObjectResult)
                {
                    TempData["Success"] = "CV uploaded successfully.";                                        
                }
                else
                {
                    TempData["Error"] = "Failed to upload CV.";
                }

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the CV: {ex.Message}");
                TempData["Error"] = "An internal error occurred.";
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Error"] = "Invalid CV ID.";
                return RedirectToPage();
            }

            try
            {
                var result = await _cvController.DeleteCvById(id);

                if (result is OkResult)
                {
                    TempData["Success"] = "CV deleted successfully.";
                }
                else
                {
                    TempData["Error"] = "Failed to delete the CV.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the CV: {ex.Message}");
                TempData["Error"] = "An internal error occurred.";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAnalyzeAsync(IFormFile cv)
        {
            try
            {                            
                var result = await _cvController.AnalyzeCv(cv);
                TempData["AnalysisResult"] = result;

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while analyzing the CV: {ex.Message}");
                TempData["Error"] = "An internal error occurred.";
                return RedirectToPage();
            }
        }





        private async Task LoadCvsAsync()
        {
            var result = await _cvController.GetAllCvs();
            if (result is OkObjectResult okResult && okResult.Value is List<CV> existingCVs)
            {
                CVs = existingCVs;
                Console.WriteLine($"CVs loaded: {CVs.Count}");
            }
            else
            {
                CVs = new List<CV>();
                Console.WriteLine("Failed to load CVs.");
            }
            return;
        }

    }
}

