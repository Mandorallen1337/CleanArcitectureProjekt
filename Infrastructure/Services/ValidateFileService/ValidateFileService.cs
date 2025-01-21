using Application.Interfaces.ValidateFileInterface;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.ValidateFileService
{
    public class ValidateFileService : IValidateFile
    {
        public async Task<bool> IsValidPDFAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
