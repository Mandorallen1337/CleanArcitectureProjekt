using Application.Interfaces.OpenAiInterface;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using MyCvSite.Controllers;
using System.Diagnostics;
using System.Security.Claims;

namespace MyCvSite.Pages
{
    [Authorize]
    public class CvModel : PageModel
    {
        private readonly CVController _cvController;
        private readonly IMemoryCache _cache;
        


        public CvModel(CVController cvController, IMemoryCache cache)
        {
            _cvController = cvController;
            _cache = cache;            
        }

        public List<CV> CVs { get; set; } = new List<CV>();
        public AnalysisResult Analysis { get; set; } = new AnalysisResult();

        private async Task LoadCvsAsync()
        {
            try
            {
                // Fetch CVs from the CV Controller
                var result = await _cvController.GetAllCvs();

                // Check if the result is valid
                if (result is OkObjectResult okResult && okResult.Value is List<CV> cvs)
                {
                    // Assign the fetched CVs to the CVs property
                    CVs = cvs;
                }
                else
                {
                    // Handle the case where the result is invalid
                    CVs = new List<CV>(); // Initialize with an empty list if no CVs are found
                }
            }
            catch (Exception ex)
            {                                
                CVs = new List<CV>(); // Initialize with an empty list if an error occurs
            }
        }


        public async Task<IActionResult> OnGet()
        {
            await LoadCvsAsync();
            return Page();
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

        public async Task<IActionResult> OnPostAnalyzeAsync(Guid cvId)
        {
            try
            {
                //Get the analysis result from the CV Controller
                var result = await _cvController.AnalyzeCv(cvId);

                //Check if the result is valid
                if (result is AnalysisResult analysisResult)
                {
                    //Assign the fetched analysis result to the Analysis property
                    Analysis = analysisResult;                    
                }
                else
                {
                    //Handle the case where the result is invalid
                    Analysis = new AnalysisResult
                    {
                        Summary = "Unable to extract useful information from CV",
                    };
                }
                await LoadCvsAsync();
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while analyzing the CV: {ex.Message}");
                TempData["Error"] = "An internal error occurred.";
                return Page();
            }

        }





        

    }
}

