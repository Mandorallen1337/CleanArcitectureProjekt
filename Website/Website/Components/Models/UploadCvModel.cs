using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Website.Components.Models
{
    public class UploadCvModel :PageModel
    {
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public List<string> UploadedCVs { get; set; } = new();

        public string SelectedCVContent { get; set; }

        public void OnGet()
        {
            // Du kan fylla `UploadedCVs` från en databas eller en fil om det behövs.
        }

        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (UploadedFile != null)
            {
                var filePath = Path.Combine("wwwroot/uploads", UploadedFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }

                UploadedCVs.Add(UploadedFile.FileName);
            }

            return RedirectToPage(); // Uppdaterar sidan
        }

        public IActionResult OnPostView(string fileName)
        {
            var filePath = Path.Combine("wwwroot/uploads", fileName);
            SelectedCVContent = System.IO.File.ReadAllText(filePath);

            return Page();
        }
    }
}
