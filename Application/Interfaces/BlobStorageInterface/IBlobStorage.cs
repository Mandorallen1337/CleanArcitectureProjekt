using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.BlobStorageInterface
{
    public interface IBlobStorage
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<Stream> DownloadFileAsync(string blobName);
        Task<string> UpdateFileAsync(string oldBlobName, IFormFile newFile);
        Task<bool> DeleteFileAsync(string blobName);
    }
}
