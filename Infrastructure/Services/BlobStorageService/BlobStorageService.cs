using Application.Interfaces.BlobStorageInterface;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.BlobStorageService
{
    public class BlobStorageService : IBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            var containerName = configuration["AzureBlobStorage:ContainerName"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Azure Blob Storage connection string is missing.");
            }

            if (string.IsNullOrEmpty(containerName))
            {
                throw new InvalidOperationException("Azure Blob Storage container name is missing.");
            }

            _containerName = containerName;
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

        public async Task<byte[]> DownloadFileAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var result = await blobClient.DownloadAsync();

            using var memoryStream = new MemoryStream();
            await result.Value.Content.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

        public async Task<bool> DeleteFileAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var response = await blobClient.DeleteIfExistsAsync();

            return response;
        }
    }
}
