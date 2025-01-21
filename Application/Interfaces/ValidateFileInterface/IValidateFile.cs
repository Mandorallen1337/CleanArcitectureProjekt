using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ValidateFileInterface
{
    public interface IValidateFile
    {
        Task<bool> IsValidPDFAsync(IFormFile file);
    }
}
