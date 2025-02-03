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

        //Uploads file to Azure and return the URL
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

        //Downloads file from Azure using its URL and returns the file as a stream
        //MemoryStream is used to temporarily store the downloaded file in memory instead of writing it to disk 
        public async Task<Stream> DownloadFileAsync(string fileUrl)
        {
            var uri = new Uri(fileUrl);
            string fileName = Path.GetFileName(uri.LocalPath);

            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var result = await blobClient.DownloadAsync();

            var memoryStream = new MemoryStream();
            await result.Value.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;            
        }

        //Deletes file from Azure if it exist and return true/false
        public async Task<bool> DeleteFileAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var response = await blobClient.DeleteIfExistsAsync();

            return response;
        }
    }
}
