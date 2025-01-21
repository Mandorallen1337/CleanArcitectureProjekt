using Application.Interfaces.ValidateFileInterface;
using Microsoft.AspNetCore.Http;
using iText.Kernel.Pdf;

namespace Infrastructure.Services.ValidateFileService
{
    public class ValidateFileService : IValidateFile
    {
        //Reads the uploaded file into memory, verifies it's a valid PDF that contains at least one page
        public async Task<bool> IsValidPDFAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
                return false;

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (var pdfReader = new PdfReader(memoryStream))
                    {
                        using (var pdfDocument = new PdfDocument(pdfReader))
                        {
                            return pdfDocument.GetNumberOfPages() > 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
